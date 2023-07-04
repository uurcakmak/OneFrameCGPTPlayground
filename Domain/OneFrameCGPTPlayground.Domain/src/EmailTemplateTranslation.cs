// -----------------------------------------------------------------------
// <copyright file="EmailTemplateTranslation.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using OneFrameCGPTPlayground.Domain.TranslationBase;
using System;

namespace OneFrameCGPTPlayground.Domain
{
    /// <summary>
    /// Email Template Translation.
    /// </summary>
    /// <seealso cref="TranslationBase.ITranslationTable{EmailTemplate, Guid}" />
    public class EmailTemplateTranslation : ITranslationTable<EmailTemplate, Guid>
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string EmailContent { get; set; }

        public Common.Enums.LanguageType Language { get; set; }

        public EmailTemplate Reference { get; set; }

        public Guid ReferenceId { get; set; }
    }
}
