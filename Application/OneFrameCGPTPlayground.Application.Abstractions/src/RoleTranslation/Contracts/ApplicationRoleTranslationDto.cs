// <copyright file="ApplicationRoleTranslationDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.TranslationBaseDto;
using OneFrameCGPTPlayground.Common.Enums;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation.Contracts
{
    /// <summary>
    /// Application Role Translation Dto.
    /// </summary>
    /// <seealso cref="Guid" />
    public class ApplicationRoleTranslationDto : ITranslationTableDto<Guid>
    {
        public string Description { get; set; }

        public string DisplayText { get; set; }

        public Guid Id { get; set; }

        public LanguageType Language { get; set; }

        public Guid ReferenceId { get; set; }
    }
}
