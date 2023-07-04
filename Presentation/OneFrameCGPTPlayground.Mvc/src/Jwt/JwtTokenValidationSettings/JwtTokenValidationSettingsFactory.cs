// <copyright file="JwtTokenValidationSettingsFactory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

namespace OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenValidationSettings
{
    public class JwtTokenValidationSettingsFactory : IJwtTokenValidationSettings
    {
        private readonly IConfiguration _configuration;

        public JwtTokenValidationSettingsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SecretKey => _configuration["Identity:Jwt:Key"];

        public bool ValidateAudience => false;

        public bool ValidateIssuer => Convert.ToBoolean(_configuration["Identity:Jwt:IssuerSettings:ValidateIssuer"], CultureInfo.InvariantCulture);

        public string ValidAudience => null;

        public string ValidIssuer => _configuration["Identity:Jwt:Issuer"];

        public TokenValidationParameters CreateTokenValidationParameters()
        {
            var result = new TokenValidationParameters
            {
                ValidateIssuer = ValidateIssuer,
                ValidIssuer = ValidIssuer,

                ValidateAudience = ValidateAudience,
                ValidAudience = ValidAudience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,
            };

            return result;
        }
    }
}
