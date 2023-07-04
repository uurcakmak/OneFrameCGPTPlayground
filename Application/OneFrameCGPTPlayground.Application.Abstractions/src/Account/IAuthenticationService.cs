// <copyright file="IAuthenticationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account
{
    /// <summary>
    /// IAuthenticationService.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IAuthenticationService : IApplicationService
    {
        /// <summary>
        /// 2FA Verification Code Sending.
        /// </summary>
        /// <param name="twoFactorVerification">The TwoFactorVerification.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task<ServiceResponse<string>> SendVerificationCodeAsync(TwoFactorVerificationDto twoFactorVerification);

        /// <summary>
        /// 2FA Verification Control.
        /// </summary>
        /// <param name="twoFactorVerification">The TwoFactorVerification.</param>
        /// <returns> A <see cref="Task" /> representing the asynchronous operation.</returns>
        Task<ServiceResponse> TwoFactorVerificationAsync(TwoFactorVerificationDto twoFactorVerification);

        /// <summary>
        /// Generate Authenticator SharedKey.
        /// </summary>
        /// <param name="userName">The User Name.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task<ServiceResponse<AuthenticatorResponseDto>> GenerateAuthenticatorSharedKeyAsync(string userName);
    }
}
