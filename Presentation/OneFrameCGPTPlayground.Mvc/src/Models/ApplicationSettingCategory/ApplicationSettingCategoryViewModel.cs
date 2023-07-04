// <copyright file="ApplicationSettingCategoryViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.ApplicationSettingCategory
{
    public class ApplicationSettingCategoryViewModel
    {
        public Guid Id { get; set; }

        [StringLength(500, ErrorMessage = "NameValidationError")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
