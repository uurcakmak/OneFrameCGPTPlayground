// <copyright file="RoleTranslationsModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Role
{
    public class RoleTranslationsModel
    {
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "DisplayText")]
        public string DisplayText { get; set; }

        [Required]
        [Display(Name = "Language")]
        public LanguageType Language { get; set; }
    }
}
