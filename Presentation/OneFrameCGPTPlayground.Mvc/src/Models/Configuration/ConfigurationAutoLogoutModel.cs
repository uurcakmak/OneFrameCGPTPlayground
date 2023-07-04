// <copyright file="ConfigurationAutoLogoutModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Configuration
{
    public class ConfigurationAutoLogoutModel
    {
        public int IdentityAutoLogoutDialogTimeout { get; set; }

        public int IdentityAutoLogoutIdleTimeout { get; set; }

        public bool IdentityAutoLogoutIsEnabled { get; set; }
    }
}
