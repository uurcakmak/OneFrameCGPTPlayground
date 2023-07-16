// <copyright file="EmailTemplateUpdateDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
using KocSistem.OneFrame.Data.Relational;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts
{
    public class EmailTemplateUpdateDto : IUpdateAuditing
    {
        public Guid Id { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public List<EmailTemplateTranslationDto> Translations { get; set; }
    }
}
