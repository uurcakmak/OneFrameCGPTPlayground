// <copyright file="UserRegisterRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class UserRegisterRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "EmailIsNotValid")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "StringLengthValidationError", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{11}$", ErrorMessage = "PhoneNumberIsNotValid")]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "StringLengthValidationError", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "StringLengthValidationError", MinimumLength = 2)]
        public string Surname { get; set; }

        public string TimeZone { get; set; }
    }
}