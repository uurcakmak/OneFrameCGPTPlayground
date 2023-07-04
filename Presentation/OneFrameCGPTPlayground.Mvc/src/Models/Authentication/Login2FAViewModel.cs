// <copyright file="Login2FAViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Authentication
{
    public class Login2FaViewModel
    {
        [Required]
        public string VerificationCode { get; set; }

        public Login2Fa VerificationType { get; set; }

        public string SharedKey { get; set; }

        public string QrCodeUri { get; set; }

        public bool? HasAuthenticatorKey { get; set; }

        public bool IsActivated { get; set; }

        public int VerificationTime { get; set; }

        public string PhoneNumberMasked { get; set; }
    }
}