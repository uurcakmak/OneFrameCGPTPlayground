// <copyright file="EmailTemplateDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.TranslationBaseDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts
{
    public class EmailTemplateDto : IDto<Guid>, IMainTableTranslationDto<EmailTemplateTranslationDto>, IInsertAuditing, IUpdateAuditing, ISoftDelete
    {
        public string Bcc { get; set; }

        public string Cc { get; set; }

        public Guid Id { get; set; }

        public DateTime? InsertedDate { get; set; }

        public string InsertedUser { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public string To { get; set; }

        public virtual List<EmailTemplateTranslationDto> Translations { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }

        public string SupportedLanguages
        {
            get
            {
                if (Translations != null && Translations.Any())
                {
                    return string.Join(", ", Translations.Select(p => p.Language).OrderBy(o => o).ToArray());
                }

                return string.Empty;
            }
        }
    }
}