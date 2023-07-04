// <copyright file="EmailTemplateGetResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;

namespace OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate
{
    public class EmailTemplateGetResponse
    {
        public string Subject { get; set; }

        public string EmailContent { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string InsertedUser { get; set; }

        public DateTime? InsertedDate { get; set; }

        public List<EmailTemplateTranslationDto> Translations { get; set; }
    }
}