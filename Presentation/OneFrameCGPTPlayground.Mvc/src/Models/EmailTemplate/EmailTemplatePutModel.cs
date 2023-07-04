// <copyright file="EmailTemplatePutModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.EmailTemplate
{
    public class EmailTemplatePutModel
    {
        public Guid Id { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public List<EmailTemplateTranslationsModel> Translations { get; set; }
    }
}