// <copyright file="ProfileModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Mvc.Models.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Profile
{
    public class ProfileModel
    {
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "EmailValidationError")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "UserNameValidationError", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [RegularExpression("(([\\+]90?)|([0]?))([ ]?)((\\([0-9]{3}\\))|([0-9]{3}))([ ]?)([0-9]{3})(\\s*[\\-]?)([0-9]{2})(\\s*[\\-]?)([0-9]{2})", ErrorMessage = "PhoneNumberIsNotValid")]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string PhoneNumberConfirmationUrl { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "UserSurnameValidationError", MinimumLength = 2)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        public bool IsActive { get; set; }

        public string ProfilePhoto { get; set; }

        public int ProfilePhotoSize { get; set; }

        public string Roles { get; set; }

        public List<TimeZoneModel> TimeZoneList { get; set; }

        public string TimeZone { get; set; }
    }
}