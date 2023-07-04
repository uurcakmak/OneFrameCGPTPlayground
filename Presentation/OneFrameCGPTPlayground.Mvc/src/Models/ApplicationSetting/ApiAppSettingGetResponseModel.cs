// <copyright file="ApiAppSettingGetResponseModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.ApplicationSetting
{
    public class ApiAppSettingGetResponseModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Key")]
        public string Key { get; set; }

        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "ValueType")]
        public string ValueType { get; set; }

        [Display(Name = "isStatic")]
        public bool IsStatic { get; set; }

        [Display(Name = "CategoryId")]
        public Guid CategoryId { get; set; }

        [Display(Name = "CategoryName")]
        public string CategoryName { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
