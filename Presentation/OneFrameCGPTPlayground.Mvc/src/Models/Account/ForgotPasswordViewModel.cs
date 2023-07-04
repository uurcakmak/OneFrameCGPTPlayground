// <copyright file="ForgotPasswordViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "EmailAddress")]
        [Required(ErrorMessage = "EmailAddressRequiredMessage")]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "EmailValidationError")]
        public string Email { get; set; }
    }
}