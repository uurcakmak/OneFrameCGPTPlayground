// <copyright file="AppSettingPutViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.ApplicationSetting
{
    public class AppSettingPutViewModel
    {
        public Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "KeyValidationError")]
        [Display(Name = "Key")]
        public string Key { get; set; }

        [StringLength(500, ErrorMessage = "ValueValidationError")]
        [Display(Name = "Value")]
        public string Value { get; set; }

        [StringLength(500, ErrorMessage = "ValueTypeValidationError")]
        [Display(Name = "ValueType")]
        public string ValueType { get; set; }

        [Display(Name = "isStatic")]
        public bool IsStatic { get; set; }

        public Guid CategoryId { get; set; }
    }
}
