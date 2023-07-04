// <copyright file="LoginResponseViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Account
{
    public class LoginResponseViewModel
    {
        public bool PasswordExpired { get; set; }

        public string RefreshToken { get; set; }

        public string Token { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
