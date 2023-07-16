// <copyright file="UserService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.Notification.Sms.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OneFrameCGPTPlayground.Application.Abstractions.User;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory.Contract;
using OneFrameCGPTPlayground.Common.Enums;
using OneFrameCGPTPlayground.Domain;
using System;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.User
{
    /// <summary>
    /// UserService.
    /// </summary>
    /// <seealso cref="IUserService" />
    public class UserService : ApplicationServiceBase<UserService>, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserConfirmationHistoryService _userConfirmationHistoryService;
        private readonly IConfiguration _configuration;
        private readonly ISmsNotificationService _smsNotificationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="userManager">The user manager.</param>
        public UserService(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, IUserConfirmationHistoryService userConfirmationHistoryService, IConfiguration configuration, ISmsNotificationService smsNotificationService)
        : base(serviceProvider)
        {
            _userManager = userManager;
            _userConfirmationHistoryService = userConfirmationHistoryService;
            _configuration = configuration;
            _smsNotificationService = smsNotificationService;
        }

        /// <summary>
        /// Gets the current user information asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see>
        ///     <cref>T:System.Threading.Tasks.Task</cref>
        /// </see>
        /// representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<UserDto>> GetCurrentUserInfoAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);
            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var data = Mapper.Map<UserDto>(user);

            return ServiceResponseHelper.SetSuccess(data);
        }

        /// <summary>
        /// Gets active confirmation code belongs to user.
        /// </summary>
        /// <param name="username">The username (email) is type of string.</param>
        /// <param name="phoneNumber">The phone number which want to be changed is type of string.</param>
        /// <returns></returns>
        public async Task<ServiceResponse<UserConfirmationHistoryDto>> GetConfirmationCodeAsync(string username, string phoneNumber)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError<UserConfirmationHistoryDto>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (!user.IsActive)
            {
                return ServiceResponseHelper.SetError<UserConfirmationHistoryDto>(null, Localize["PassiveUser"], StatusCodes.Status204NoContent, true);
            }

            if (user.PhoneNumberConfirmed)
            {
                return ServiceResponseHelper.SetError<UserConfirmationHistoryDto>(null, Localize["PhoneNumberAlreadyConfirmed"], StatusCodes.Status204NoContent, true);
            }

            var codeResponse = await _userConfirmationHistoryService.GetActiveCodeAsync(user.Id, phoneNumber).ConfigureAwait(false);

            if (codeResponse.IsSuccessful)
            {
                var code = codeResponse.Result;

                if (code == null)
                {
                    var newCode = await GenerateNewCodeAsync(user, phoneNumber).ConfigureAwait(false);
                    var response = await SaveNewCodeAsync(user, phoneNumber, newCode).ConfigureAwait(false);
                    code = response.Result;
                }

                return ServiceResponseHelper.SetSuccess(code);
            }
            else
            {
                return ServiceResponseHelper.SetError<UserConfirmationHistoryDto>(null, Localize["DefaultError"], StatusCodes.Status400BadRequest, true);
            }
        }

        /// <summary>
        /// Sends confirmation code to user.
        /// </summary>
        /// <param name="code">The confirmation code is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        public async Task<ServiceResponse> SendConfirmationCodeAsync(UserConfirmationHistoryDto code)
        {
            string content = CreateConfirmationSmsContent(code.Code);

            var smsResponse = await _smsNotificationService.SendSmsAsync(new SendSmsRequest()
            {
                Sender = _configuration["SMSNotificationSettings:Sender"],
                Text = content,
                Receiver = code.PhoneNumber,
                TrackId = Guid.NewGuid(),
            }).ConfigureAwait(false);

            if (!smsResponse.IsSuccessful)
            {
                return ServiceResponseHelper.SetError(smsResponse.ErrorMessage);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Checks confirmation code as sent.
        /// </summary>
        /// <param name="code">The confirmation code is type of UserConfirmationCodeDto.</param>
        /// <returns></returns>
        public async Task<ServiceResponse> CheckConfirmationCodeAsync(UserConfirmationHistoryDto code)
        {
            var checkingResponse = await _userConfirmationHistoryService.CheckCodeAsSentAsync(code.Id).ConfigureAwait(false);

            if (!checkingResponse.IsSuccessful)
            {
                return ServiceResponseHelper.SetError(checkingResponse.Error);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Confirms user phone number confirmation code.
        /// </summary>
        /// <param name="code">The object is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        public async Task<ServiceResponse> ConfirmCodeAsync(UserConfirmationHistoryDto code)
        {
            var confirmationResponse = await _userConfirmationHistoryService.ConfirmCodeAsync(code).ConfigureAwait(false);

            if (!confirmationResponse.IsSuccessful)
            {
                return ServiceResponseHelper.SetError(confirmationResponse.Error);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Checks user's confirmation as done.
        /// </summary>
        /// <param name="username">The username is type of string.</param>
        /// <returns></returns>
        public async Task<ServiceResponse> CheckUserConfirmationAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (!user.IsActive)
            {
                return ServiceResponseHelper.SetError(Localize["PassiveUser"], StatusCodes.Status400BadRequest, true);
            }

            user.PhoneNumberConfirmed = true;
            await _userManager.UpdateAsync(user).ConfigureAwait(false);

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Generates new confirmation code.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="phoneNumber">The phone number which want to be changed.</param>
        /// <returns></returns>
        private async Task<string> GenerateNewCodeAsync(ApplicationUser user, string phoneNumber)
        {
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber).ConfigureAwait(false);

            return code;
        }

        /// <summary>
        /// Saves new code to db.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="phoneNumber">The phone number which want to be changed.</param>
        /// <param name="newCode">The new code.</param>
        /// <returns></returns>
        private async Task<ServiceResponse<UserConfirmationHistoryDto>> SaveNewCodeAsync(ApplicationUser user, string phoneNumber, string newCode)
        {
            var seconds = _configuration["PhoneNumberConfirmationSettings:SecondsForCodeExpired"];

            DateTime expiredDate = DateTime.UtcNow.AddSeconds(Convert.ToDouble(seconds));

            var response = await _userConfirmationHistoryService.CreateCodeAsync(new UserConfirmationHistoryDto
            {
                UserId = user.Id,
                Code = newCode,
                CodeType = (int)ConfirmationType.PhoneNumber,
                PhoneNumber = phoneNumber,
                ExpiredDate = expiredDate
            }).ConfigureAwait(false);

            return response;
        }

        /// <summary>
        /// Creates sms content for sending confirmation code.
        /// </summary>
        /// <param name="code">The confirmation code.</param>
        /// <returns></returns>
        private string CreateConfirmationSmsContent(string code)
        {
            string confirmationContentTask = Localize["PhoneNumberConfirmationCode"];

            return $"{confirmationContentTask} {code}";
        }
    }
}