// <copyright file="AuthenticationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Account;
using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate;
using OneFrameCGPTPlayground.Application.Abstractions.Notification;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Common.Enums;
using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.Notification.Sms.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Account
{
    /// <summary>
    /// AccountService.
    /// </summary>
    /// <seealso cref="IAuthenticationService" />
    public class AuthenticationService : ApplicationServiceBase<AuthenticationService>, IAuthenticationService
    {
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<IdentityUserToken> _userToken;
        private readonly ISmsNotificationService _smsNotification;
        private readonly IConfiguration _configuration;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IApplicationSettingService _applicationSettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        public AuthenticationService(
            IServiceProvider serviceProvider,
            UserManager<ApplicationUser> userManager,
            IEmailNotificationService emailNotificationService,
            IRepository<IdentityUserToken> userToken,
            ISmsNotificationService smsNotification,
            IConfiguration configuration,
            IEmailTemplateService emailTemplateService,
            IApplicationSettingService applicationSettingService)
        : base(serviceProvider)
        {
            _userManager = userManager;
            _emailNotificationService = emailNotificationService;
            _userToken = userToken;
            _smsNotification = smsNotification;
            _configuration = configuration;
            _emailTemplateService = emailTemplateService;
            _applicationSettingService = applicationSettingService;
        }

        /// <summary>
        /// 2FA Verification Code Sending.
        /// </summary>
        /// <param name="twoFactorVerification">The TwoFactorVerification.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<string>> SendVerificationCodeAsync(TwoFactorVerificationDto twoFactorVerification)
        {
            var user = await _userManager.FindByNameAsync(twoFactorVerification.Username).ConfigureAwait(false);

            var userToken = _userToken.GetFirstOrDefaultAsync(predicate: i => i.UserId == user.Id).Result;
            var applicationSettingResponse = await _applicationSettingService.GetByKeyAsync(ConfigurationConstant.Identity2FaSettingsVerificationTime).ConfigureAwait(false);
            if (userToken == null || DateTime.UtcNow > userToken.SentDate.AddSeconds(applicationSettingResponse.Result.Value))
            {
                switch (twoFactorVerification.VerificationType)
                {
                    case Login2Fa.Sms:
                        return await SendSmsVerificationCodeAsync(user).ConfigureAwait(false);
                    case Login2Fa.Email:
                        return await SendEmailVerificationCodeAsync(user).ConfigureAwait(false);
                    default:
                        return ServiceResponseHelper.SetError<string>(null, Localize["VerificationTypeNotDefined"], StatusCodes.Status400BadRequest, true);
                }
            }

            return ServiceResponseHelper.SetSuccess(string.Empty);
        }

        /// <summary>
        /// 2FA Verification Control.
        /// </summary>
        /// <param name="twoFactorVerification">The TwoFactorVerification.</param>
        /// <returns> A <see cref="Task" /> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse> TwoFactorVerificationAsync(TwoFactorVerificationDto twoFactorVerification)
        {
            var user = await _userManager.FindByNameAsync(twoFactorVerification.Username).ConfigureAwait(false);

            var tokenName = string.Empty;
            switch (twoFactorVerification.VerificationType)
            {
                case Login2Fa.Sms:
                    tokenName = TokenOptions.DefaultPhoneProvider;
                    break;

                case Login2Fa.Email:
                    tokenName = TokenOptions.DefaultEmailProvider;
                    break;

                case Login2Fa.Authenticator:
                    tokenName = AuthenticationConstant.KsAuthenticatorProvider;
                    break;

                default:
                    return ServiceResponseHelper.SetError(Localize["VerificationTypeNotDefined"], StatusCodes.Status400BadRequest, true);
            }

            var userTokenModel = _userToken.GetFirstOrDefault(predicate: x => x.UserId == user.Id && x.LoginProvider == AuthenticationConstant.KsLoginProvider && x.Name == tokenName, disableTracking: false);

            var control = twoFactorVerification.VerificationType != Login2Fa.Authenticator ? userTokenModel.Value == twoFactorVerification.VerificationCode :
                await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultAuthenticatorProvider, twoFactorVerification.VerificationCode).ConfigureAwait(false);

            if (control)
            {
                userTokenModel.IsActivated = true;
                await _userToken.UpdateAsync(userTokenModel).ConfigureAwait(false);

                return ServiceResponseHelper.SetSuccess();
            }

            return ServiceResponseHelper.SetError(Localize["InvalidVerificationCode"], StatusCodes.Status400BadRequest, true);
        }

        /// <summary>
        /// Generate Authenticator SharedKey.
        /// </summary>
        /// <param name="userName">The User Name.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<AuthenticatorResponseDto>> GenerateAuthenticatorSharedKeyAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            var responseDto = new AuthenticatorResponseDto()
            {
                SharedKey = await _userManager.GetAuthenticatorKeyAsync(user).ConfigureAwait(false),
            };

            if (string.IsNullOrEmpty(responseDto.SharedKey))
            {
                responseDto.HasAuthenticatorKey = false;

                var result = await _userManager.ResetAuthenticatorKeyAsync(user).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    responseDto.SharedKey = await _userManager.GetAuthenticatorKeyAsync(user).ConfigureAwait(false);
                }
            }
            else
            {
                responseDto.HasAuthenticatorKey = true;
            }

            var userTokenModel = _userToken.GetFirstOrDefault(predicate: x => x.UserId == user.Id && x.LoginProvider == AuthenticationConstant.KsLoginProvider && x.Name == AuthenticationConstant.KsAuthenticatorProvider);
            responseDto.IsActivated = userTokenModel.IsActivated;
            return ServiceResponseHelper.SetSuccess(responseDto);
        }

        private async Task<ServiceResponse<string>> SendSmsVerificationCodeAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider).ConfigureAwait(false);
            var smsKeySaveResult = await _userManager.SetAuthenticationTokenAsync(user, AuthenticationConstant.KsLoginProvider, TokenOptions.DefaultPhoneProvider, token).ConfigureAwait(false);

            var userToken = _userToken.GetFirstOrDefault(predicate: x => x.UserId == user.Id && x.LoginProvider == AuthenticationConstant.KsLoginProvider && x.Name == TokenOptions.DefaultPhoneProvider, disableTracking: false);
            userToken.SentDate = DateTime.UtcNow;
            await _userToken.UpdateAsync(userToken).ConfigureAwait(false);

            if (smsKeySaveResult.Succeeded)
            {
                _ = await _smsNotification.SendSmsAsync(new SendSmsRequest()
                {
                    Sender = _configuration["NotificationSettings:SMS:Sender"],
                    Text = Localize["SMSVerificationCodeMessage"] + token,
                    Receiver = user.PhoneNumber,
                    TrackId = Guid.NewGuid(),
                }).ConfigureAwait(false);

                var phoneMasked = !string.IsNullOrEmpty(user.PhoneNumber) && user.PhoneNumber.Length > 4 ? user.PhoneNumber.Substring(user.PhoneNumber.Length - 4, 4) : string.Empty;
                return ServiceResponseHelper.SetSuccess<string>($"*******{phoneMasked}");
            }

            return ServiceResponseHelper.SetError<string>(null, Localize["VerificationTypeNotDefined"], StatusCodes.Status400BadRequest, true);
        }

        private async Task<ServiceResponse<string>> SendEmailVerificationCodeAsync(ApplicationUser user)
        {
            var currentTwoLetterCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            Enum.TryParse(typeof(Common.Enums.LanguageType), currentTwoLetterCulture, false, out object languageEnum);
            Common.Enums.LanguageType cultureLanguageEnum = (Common.Enums.LanguageType)languageEnum;

            var emailTemplateResponse = await _emailTemplateService.GetEmailTemplateByNameAsync(EmailTemplateName.TwoFAVerificationCode).ConfigureAwait(false);
            if (!emailTemplateResponse.IsSuccessful)
            {
                return ServiceResponseHelper.SetError<string>(null, Localize["EmailTemplateNotFound"], StatusCodes.Status204NoContent, true);
            }

            var emailTranslation = emailTemplateResponse.Result.Translations.FirstOrDefault(x => x.Language == cultureLanguageEnum);
            if (emailTranslation == null)
            {
                return ServiceResponseHelper.SetError<string>(null, Localize["EmailTemplateNotFound"], StatusCodes.Status204NoContent, true);
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider).ConfigureAwait(false);
            var emailKeySaveResult = await _userManager.SetAuthenticationTokenAsync(user, AuthenticationConstant.KsLoginProvider, TokenOptions.DefaultEmailProvider, token).ConfigureAwait(false);

            var userToken = _userToken.GetFirstOrDefault(predicate: x => x.UserId == user.Id && x.LoginProvider == AuthenticationConstant.KsLoginProvider && x.Name == TokenOptions.DefaultEmailProvider, disableTracking: false);
            userToken.SentDate = DateTime.UtcNow;
            await _userToken.UpdateAsync(userToken).ConfigureAwait(false);

            if (!emailKeySaveResult.Succeeded)
            {
                return ServiceResponseHelper.SetError<string>(null, Localize["VerificationCodeNotRetrieved"], StatusCodes.Status400BadRequest, true);
            }

            emailTranslation.EmailContent = emailTranslation.EmailContent.Replace("{token}", token, StringComparison.CurrentCulture);
            await _emailNotificationService.SendEmailAsync(emailTranslation.Subject, emailTranslation.EmailContent, user.Email).ConfigureAwait(false);
            return ServiceResponseHelper.SetSuccess<string>(null);
        }
    }
}