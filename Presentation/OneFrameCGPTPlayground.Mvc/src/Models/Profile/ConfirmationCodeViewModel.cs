// <copyright file="ConfirmationCodeViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Profile
{
    public class ConfirmationCodeViewModel
    {
        [Required(ErrorMessage = "ConfirmCodeShouldBeFilled")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "PhoneNumberConfirmationCodeError")]
        [DataType(DataType.Text)]
        [Display(Name = "ConfirmationCode")]
        public string Code { get; set; }

        public long ExpiredDate { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        public Guid Id { get; set; }
    }
}