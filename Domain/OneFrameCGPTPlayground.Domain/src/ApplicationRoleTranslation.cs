// <copyright file="ApplicationRoleTranslation.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Domain.TranslationBase;
using System;

namespace OneFrameCGPTPlayground.Domain
{
    /// <summary>
    /// Application Role Translation.
    /// </summary>
    public class ApplicationRoleTranslation : ITranslationTable<ApplicationRole, Guid>
    {
        public string Description { get; set; }

        public string DisplayText { get; set; }

        public Guid Id { get; set; }

        public Common.Enums.LanguageType Language { get; set; }

        public ApplicationRole Reference { get; set; }

        public Guid ReferenceId { get; set; }
    }
}