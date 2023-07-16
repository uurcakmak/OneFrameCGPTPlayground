// <copyright file="IUserService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory.Contract;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.User
{
    /// <summary>
    /// IUserService.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IUserService : IApplicationService
    {
        /// <summary>
        /// Gets the current user information asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<UserDto>> GetCurrentUserInfoAsync(string username);

        /// <summary>
        /// Gets active confirmation code belongs to user.
        /// </summary>
        /// <param name="username">The username (email) is type of string.</param>
        /// <param name="phoneNumber">The phone number which want to be changed is type of string.</param>
        /// <returns></returns>
        Task<ServiceResponse<UserConfirmationHistoryDto>> GetConfirmationCodeAsync(string username, string phoneNumber);

        /// <summary>
        /// Sends confirmation code to user.
        /// </summary>
        /// <param name="code">The confirmation code is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        Task<ServiceResponse> SendConfirmationCodeAsync(UserConfirmationHistoryDto code);

        /// <summary>
        /// Checks confirmation code as sent.
        /// </summary>
        /// <param name="code">The confirmation code is type of UserConfirmationCodeDto.</param>
        /// <returns></returns>
        Task<ServiceResponse> CheckConfirmationCodeAsync(UserConfirmationHistoryDto code);

        /// <summary>
        /// Confirms user phone number confirmation code.
        /// </summary>
        /// <param name="code">The object is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        Task<ServiceResponse> ConfirmCodeAsync(UserConfirmationHistoryDto code);

        /// <summary>
        /// Checks user's confirmation as done.
        /// </summary>
        /// <param name="username">The username is type of string.</param>
        /// <returns></returns>
        Task<ServiceResponse> CheckUserConfirmationAsync(string username);
    }
}