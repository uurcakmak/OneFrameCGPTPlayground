// <copyright file="LanguagePutModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Language
{
    public class LanguagePutModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }
    }
}
