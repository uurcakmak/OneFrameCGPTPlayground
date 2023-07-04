// <copyright file="AuthenticationSettingModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Jwt.AuthenticationSettings
{
    public class AuthenticationSettingModel
    {
        public string LoginPath { get; set; }

        public string AccessDeniedPath { get; set; }
    }
}