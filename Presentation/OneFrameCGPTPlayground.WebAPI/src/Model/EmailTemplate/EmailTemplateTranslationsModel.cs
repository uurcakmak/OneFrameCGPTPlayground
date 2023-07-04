// <copyright file="EmailTemplateTranslationsModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate
{
    public class EmailTemplateTranslationsModel
    {
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "EmailContent")]
        public string EmailContent { get; set; }

        [Required]
        public LanguageType Language { get; set; }
    }
}
