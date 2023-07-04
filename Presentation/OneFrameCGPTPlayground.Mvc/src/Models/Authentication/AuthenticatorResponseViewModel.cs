// <copyright file="AuthenticatorResponseViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Authentication
{
    public class AuthenticatorResponseViewModel
    {
        public bool HasAuthenticatorKey { get; set; }

        public string SharedKey { get; set; }

        public bool IsActivated { get; set; }
    }
}
