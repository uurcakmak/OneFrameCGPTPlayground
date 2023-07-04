// <copyright file="TokenHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Authentication.Interfaces;
using KocSistem.OneFrame.Authentication.Utilities;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.DesignObjects.Models;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using System.Collections.Generic;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.Infrastructure.Helpers
{
    /// <summary>
    /// TokenHelper.
    /// </summary>
    /// <seealso cref="ITokenHelper" />
    public class TokenHelper : ITokenHelper
    {
        private readonly IKsStringLocalizer<TokenHelper> _localize;
        private readonly IServiceResponseHelper _serviceResponseHelper;
        private readonly ITokenBuilderService _tokenBuilderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenHelper"/> class.
        /// </summary>
        /// <param name="i18N">The i18 n.</param>
        /// <param name="serviceResponseHelper">The service response helper.</param>
        /// <param name="tokenBuilderService">tokenBuilderService.</param>
        public TokenHelper(IKsI18N i18N, IServiceResponseHelper serviceResponseHelper, ITokenBuilderService tokenBuilderService)
        {
            _ = i18N.ThrowIfNull(nameof(i18N));

            _localize = i18N.GetLocalizer<TokenHelper>();
            _serviceResponseHelper = serviceResponseHelper;
            _tokenBuilderService = tokenBuilderService;
        }

        /// <summary>
        /// Builds the token.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <returns>ServiceResponse{AccessToken}.</returns>
        public ServiceResponse<AccessToken> BuildToken(IList<Claim> claims, int? expireInMinutes = null, int? refreshTokenExpireInMinutes = null)
        {
            var token = _tokenBuilderService.BuildToken(claims, expireInMinutes, refreshTokenExpireInMinutes);
            if (token.IsSuccessful)
            {
                var data = new AccessToken() { Token = token.Result.Token, RefreshToken = token.Result.RefreshToken };

                return _serviceResponseHelper.SetSuccess(data);
            }

            if (!token.IsSuccessful)
            {
                return _serviceResponseHelper.SetError<AccessToken>(null, token.Error);
            }

            return _serviceResponseHelper.SetError<AccessToken>(null, token.Error);
        }

        /// <summary>
        /// Validates the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>ServiceResponse{AccessToken}.</returns>
        public ServiceResponse<AccessToken> ValidateRefreshToken(string refreshToken)
        {
            var refreshResult = _tokenBuilderService.ValidateRefreshToken(refreshToken);

            if (!refreshResult.IsSuccessful)
            {
                var errorInfo = new ErrorInfo(_localize[refreshResult.Error.Message]);

                return _serviceResponseHelper.SetError<AccessToken>(null, errorInfo, true);
            }

            return refreshResult;
        }
    }
}