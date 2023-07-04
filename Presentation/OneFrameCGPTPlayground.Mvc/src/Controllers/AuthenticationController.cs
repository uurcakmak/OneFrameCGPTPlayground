// <copyright file="AuthenticationController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Common.Enums;
using OneFrameCGPTPlayground.Mvc.Extensions;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.Account;
using OneFrameCGPTPlayground.Mvc.Models.Authentication;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text.Encodings.Web;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("authentications")]
    public class AuthenticationController : BaseController<AuthenticationController>
    {
        private readonly IKsStringLocalizer<AuthenticationController> _localize;
        private readonly IClaimHelper _claimHelper;
        private readonly UrlEncoder _urlEncoder;

        public AuthenticationController(IKsI18N i18N, UrlEncoder urlEncoder, IClaimHelper claimHelper)
            : base(i18N)
        {
            _localize = i18N.GetLocalizer<AuthenticationController>();
            _urlEncoder = urlEncoder;
            _claimHelper = claimHelper;
        }

        [HttpGet("login-2fa")]
        [AllowAnonymous]
        public async Task<IActionResult> Login2FaAsync()
        {
            var dbConfiguration = GetConfigurationsAsync(new List<string>
            {
                ConfigurationConstant.Identity2FaSettingsType,
                ConfigurationConstant.Identity2FaSettingsVerificationTime,
                ConfigurationConstant.Identity2FaSettingsAuthenticatorLinkName,
            }).Result;

            var login = TempData.Get<LoginViewModel>("LoginModel");
            var twoFactorModel = new Login2FaViewModel
            {
                VerificationType = Enum.Parse<Login2Fa>(dbConfiguration.Identity2FaSettingsType),
                VerificationTime = dbConfiguration.Identity2FaSettingsVerificationTime,
            };

            if (twoFactorModel.VerificationType == Common.Enums.Login2Fa.Authenticator)
            {
                var response = (await PostApiRequestWithAllHeadersAsync<ServiceResponse<AuthenticatorResponseViewModel>>(ApiEndpoints.AuthenticationGenerateAuthenticatorSharedKey, login.Email).ConfigureAwait(false)).Result;
                var qrcodeUri = $"otpauth://totp/{_urlEncoder.Encode(dbConfiguration.Identity2FaSettingsAuthenticatorLinkName)}:{_urlEncoder.Encode(login.Email)}?secret={response.SharedKey}&issuer={_urlEncoder.Encode(dbConfiguration.Identity2FaSettingsAuthenticatorLinkName)}";
                twoFactorModel.HasAuthenticatorKey = response.HasAuthenticatorKey;
                twoFactorModel.SharedKey = response.SharedKey;
                twoFactorModel.QrCodeUri = qrcodeUri;
                twoFactorModel.IsActivated = response.IsActivated;
            }
            else
            {
                var model = new TwoFactorVerificationDto { Username = login.Email, VerificationType = twoFactorModel.VerificationType };

                var serviceResponse = await PostApiRequestWithAllHeadersAsync<ServiceResponse<string>>(ApiEndpoints.AuthenticationSendVerificationCode, model).ConfigureAwait(false);
                twoFactorModel.PhoneNumberMasked = serviceResponse?.Result;
            }

            TempData.Put("LoginModel", login);
            return View(twoFactorModel);
        }

        [HttpPost("login-2fa")]
        [AllowAnonymous]
        public async Task<IActionResult> Login2FaAsync(Login2FaViewModel model)
        {
            var login = TempData.Get<LoginViewModel>("LoginModel");

            var dbConfiguration = GetConfigurationsAsync(new List<string>
            {
                ConfigurationConstant.Identity2FaSettingsType,
            }).Result;

            var twoFactorModel = new TwoFactorVerificationDto
            {
                VerificationCode = model.VerificationCode,
                VerificationType = Enum.Parse<Login2Fa>(dbConfiguration.Identity2FaSettingsType),
                Username = login.Email,
            };

            var verificationResponse = await PostApiRequestWithAllHeadersAsync<ServiceResponse>(ApiEndpoints.AuthenticationTwoFactorVerification, twoFactorModel).ConfigureAwait(false);

            if (verificationResponse.IsSuccessful)
            {
                var response = await PostApiRequestWithAllHeadersAsync<ServiceResponse<LoginResponseViewModel>>(ApiEndpoints.AccountLogin, login).ConfigureAwait(false);
                if (!response.IsSuccessful)
                {
                    return ToastError(response.Error);
                }

                await _claimHelper.BuildClaimsAndSignIn(response.Result).ConfigureAwait(false);

                return ToastSuccessForRedirect(_localize["LoginSuccess"], !string.IsNullOrEmpty(login.ReturnUrl) ? login.ReturnUrl : Url.Action("Index", "Home"));
            }
            else
            {
                TempData.Put("LoginModel", login);

                return ToastError(_localize["ValidationError"] + verificationResponse.Error.Message);
            }
        }
    }
}