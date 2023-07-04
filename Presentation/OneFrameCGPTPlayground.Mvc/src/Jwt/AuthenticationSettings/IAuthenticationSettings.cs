// <copyright file="IAuthenticationSettings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Jwt.AuthenticationSettings
{
    public interface IAuthenticationSettings
    {
        PathString LoginPath { get; }

        PathString AccessDeniedPath { get; }
    }
}