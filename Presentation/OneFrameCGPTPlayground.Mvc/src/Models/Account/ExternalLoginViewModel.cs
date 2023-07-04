// <copyright file="ExternalLoginViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Account
{
    public class ExternalLoginViewModel
    {
        public string Provider { get; set; }

        public string ReturnUrl { get; set; }
    }
}