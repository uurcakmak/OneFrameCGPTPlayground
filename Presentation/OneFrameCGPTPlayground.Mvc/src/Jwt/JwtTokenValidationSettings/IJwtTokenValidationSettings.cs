// <copyright file="IJwtTokenValidationSettings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.IdentityModel.Tokens;

namespace OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenValidationSettings
{
    public interface IJwtTokenValidationSettings
    {
        string ValidIssuer { get; }

        bool ValidateIssuer { get; }

        string ValidAudience { get; }

        bool ValidateAudience { get; }

        string SecretKey { get; }

        TokenValidationParameters CreateTokenValidationParameters();
    }
}