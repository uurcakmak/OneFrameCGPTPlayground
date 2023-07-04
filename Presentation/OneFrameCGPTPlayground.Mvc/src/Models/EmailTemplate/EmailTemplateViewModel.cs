// <copyright file="EmailTemplateViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.EmailTemplate
{
    public class EmailTemplateViewModel
    {
        public EmailTemplateViewModel()
        {
            this.Translations = new List<EmailTemplateTranslationsModel>();
        }

        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "To")]
        public string To { get; set; }

        [Display(Name = "Cc")]
        public string Cc { get; set; }

        [Display(Name = "Bcc")]
        public string Bcc { get; set; }

        [Display(Name = "UpdatedUser")]
        public string UpdatedUser { get; set; }

        [Display(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        public List<EmailTemplateTranslationsModel> Translations { get; set; }
    }
}
