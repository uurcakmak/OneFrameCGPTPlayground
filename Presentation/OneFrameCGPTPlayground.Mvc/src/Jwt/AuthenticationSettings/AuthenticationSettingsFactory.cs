// <copyright file="AuthenticationSettingsFactory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;

namespace OneFrameCGPTPlayground.Mvc.Jwt.AuthenticationSettings
{
    public class AuthenticationSettingsFactory : IAuthenticationSettings
    {
        public AuthenticationSettingsFactory(IConfiguration configuration)
        {
            _ = configuration.ThrowIfNull(nameof(configuration));
            LoginPath = new PathString(configuration["Identity:Jwt:IssuerSettings:Login"]);
            AccessDeniedPath = new PathString(configuration["Identity:Jwt:IssuerSettings:AccessDeniedPath"]);
        }

        public PathString AccessDeniedPath { get; private set; }

        public PathString LoginPath { get; private set; }
    }
}