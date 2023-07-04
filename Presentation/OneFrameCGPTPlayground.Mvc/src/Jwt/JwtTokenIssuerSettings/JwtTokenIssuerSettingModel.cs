// <copyright file="JwtTokenIssuerSettingModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenIssuerSettings
{
    public class JwtTokenIssuerSettingModel
    {
        public string BaseAddress { get; set; }

        public string Login { get; set; }

        public string RenewToken { get; set; }
    }
}