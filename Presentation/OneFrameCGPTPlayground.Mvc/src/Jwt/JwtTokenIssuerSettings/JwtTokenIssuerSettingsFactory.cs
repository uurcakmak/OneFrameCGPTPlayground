// <copyright file="JwtTokenIssuerSettingsFactory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenIssuerSettings
{
    public class JwtTokenIssuerSettingsFactory : IJwtTokenIssuerSettings
    {
        private readonly IConfiguration _configuration;

        public JwtTokenIssuerSettingsFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseAddress => _configuration["Identity:Jwt:IssuerSettings:BaseAddress"];

        public string Login => _configuration["Identity:Jwt:IssuerSettings:Login"];

        public string RenewToken => _configuration["Identity:Jwt:IssuerSettings:RenewToken"];
    }
}
