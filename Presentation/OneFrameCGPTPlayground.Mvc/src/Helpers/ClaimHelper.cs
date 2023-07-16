// <copyright file="ClaimHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Authentication.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenValidationSettings;
using OneFrameCGPTPlayground.Mvc.Models.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.Mvc.Helpers
{
    public class ClaimHelper : IClaimHelper
    {
        private readonly IJwtTokenValidationSettings _jwtTokenValidationSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimHelper(IJwtTokenValidationSettings jwtTokenValidationSettings, IHttpContextAccessor httpContextAccessor)
        {
            _jwtTokenValidationSettings = jwtTokenValidationSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task BuildClaimsAndSignIn(LoginResponseViewModel loginResponse)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.ReadToken(loginResponse.Token) as JwtSecurityToken;
            var principal = tokenHandler.ValidateToken(loginResponse.Token, _jwtTokenValidationSettings.CreateTokenValidationParameters(), out var validatedToken);
            var identity = principal.Identity as ClaimsIdentity;

            // Search for missed claims, for example claim 'sub'
            var extraClaims = token.Claims.Where(c => !identity.Claims.Any(x => x.Type == c.Type)).ToList();
            extraClaims.Add(new Claim(JwtGrantType.AccessToken, loginResponse.Token));
            extraClaims.Add(new Claim(JwtGrantType.RefreshToken, loginResponse.RefreshToken));

            identity.AddClaims(extraClaims);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UnixEpoch.AddSeconds(token.Payload.Exp.Value),
                IsPersistent = true,
            };

            await _httpContextAccessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties).ConfigureAwait(false);
        }
    }
}