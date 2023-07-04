// <copyright file="AccountResetPasswordRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Infrastructure.Helpers;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountResetPasswordRequestExample : IExamplesProvider<ResetPasswordRequest>
    {
        private readonly ITokenHelper _tokenHelper;

        public AccountResetPasswordRequestExample(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        public ResetPasswordRequest GetExamples()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "ghostbusters@kocsistem.com.tr"),
                new Claim(JwtRegisteredClaimNames.UniqueName, "oneframe"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
            var token = _tokenHelper.BuildToken(claims);

            var password = Guid.NewGuid().ToString().Remove(5);

            return new ResetPasswordRequest
            {
                Token = token.Result.Token,
                RefreshToken = token.Result.RefreshToken,
                Email = "ghostbusters@kocsistem.com.tr",
                Password = password,
                ConfirmPassword = password,
            };
        }
    }
}