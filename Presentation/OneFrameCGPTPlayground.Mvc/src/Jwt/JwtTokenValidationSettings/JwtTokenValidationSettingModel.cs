// <copyright file="JwtTokenValidationSettingModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenValidationSettings
{
    public class JwtTokenValidationSettingModel
    {
        public string ValidIssuer { get; set; }

        public bool ValidateIssuer { get; set; }

        public string ValidAudience { get; set; }

        public bool ValidateAudience { get; set; }

        public string SecretKey { get; set; }
    }
}