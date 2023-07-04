// <copyright file="IJwtTokenIssuerSettings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenIssuerSettings
{
    public interface IJwtTokenIssuerSettings
    {
        string BaseAddress { get; }

        string Login { get; }

        string RenewToken { get; }
    }
}