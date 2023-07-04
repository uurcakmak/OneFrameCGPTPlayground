// <copyright file="EmailTemplateTranslationDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.TranslationBaseDto;
using OneFrameCGPTPlayground.Common.Enums;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts
{
    /// <summary>
    /// Email Template Translation Dto.
    /// </summary>
    /// <seealso cref="Guid" />
    public class EmailTemplateTranslationDto : ITranslationTableDto<Guid>
    {
        public string Subject { get; set; }

        public string EmailContent { get; set; }

        public Guid Id { get; set; }

        public LanguageType Language { get; set; }

        public Guid ReferenceId { get; set; }
    }
}
