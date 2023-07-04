// <copyright file="UserViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.User
{
    public class UserViewModel
    {
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "EmailValidationError")]
        public string Email { get; set; }

        public string Id { get; set; }

        [StringLength(50, ErrorMessage = "UserNameValidationError")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0-9]{11}$", ErrorMessage = "PhoneNumberIsNotValid")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "UserSurnameValidationError")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "IsLocked")]
        public bool IsLocked { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "TimeZone")]
        public string TimeZone { get; set; }
    }
}