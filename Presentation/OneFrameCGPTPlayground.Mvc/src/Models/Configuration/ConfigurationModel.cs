// <copyright file="ConfigurationModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace OneFrameCGPTPlayground.Mvc.Models.Configuration
{
    public class ConfigurationModel
    {
        [JsonProperty("Identity:2FASettings:AuthenticatorLinkName")]
        public string Identity2FaSettingsAuthenticatorLinkName { get; set; }

        [JsonProperty("Identity:2FASettings:IsEnabled")]
        public bool Identity2FaSettingsIsEnabled { get; set; }

        [JsonProperty("Identity:AutoLogout:DialogTimeout")]
        public int IdentityAutoLogoutDialogTimeout { get; set; }

        [JsonProperty("Identity:2FASettings:VerificationTime")]
        public int Identity2FaSettingsVerificationTime { get; set; }

        [JsonProperty("Identity:AutoLogout:IdleTimeout")]
        public int IdentityAutoLogoutIdleTimeout { get; set; }

        [JsonProperty("Identity:2FASettings:Type")]
        public string Identity2FaSettingsType { get; set; }

        [JsonProperty("Identity:AutoLogout:IsEnabled")]
        public bool IdentityAutoLogoutIsEnabled { get; set; }
    }
}