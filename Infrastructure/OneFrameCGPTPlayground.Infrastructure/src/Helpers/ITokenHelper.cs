// <copyright file="ITokenHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Authentication.Utilities;
using KocSistem.OneFrame.DesignObjects.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.Infrastructure.Helpers
{
    /// <summary>
    /// ITokenHelper.
    /// </summary>
    public interface ITokenHelper
    {
        /// <summary>
        /// Builds the token.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <param name="expireInMinutes">The expire in minutes.</param>
        /// <param name="refreshTokenExpireInMinutes">The refresh token expire in minutes.</param>
        /// <returns>ServiceResponse{AccessToken}.</returns>
        ServiceResponse<AccessToken> BuildToken(IList<Claim> claims, int? expireInMinutes = null, int? refreshTokenExpireInMinutes = null);

        /// <summary>
        /// Validates the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>ServiceResponse{AccessToken}.</returns>
        ServiceResponse<AccessToken> ValidateRefreshToken(string refreshToken);
    }
}