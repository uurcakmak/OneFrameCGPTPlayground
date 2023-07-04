// <copyright file="EmailTemplateGetWithTranslatesResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate
{
    public class EmailTemplateGetWithTranslatesResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<EmailTemplateTranslationsModel> Translations { get; set; }
    }
}
