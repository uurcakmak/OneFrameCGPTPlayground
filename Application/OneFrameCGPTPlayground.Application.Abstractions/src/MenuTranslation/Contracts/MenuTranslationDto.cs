// <copyright file="MenuTranslationDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.TranslationBaseDto;
using OneFrameCGPTPlayground.Common.Enums;

namespace OneFrameCGPTPlayground.Application.Abstractions.MenuTranslation.Contracts
{
    /// <summary>
    /// Menu Translation Dto.
    /// </summary>
    /// <seealso cref="int" />
    public class MenuTranslationDto : ITranslationTableDto<int>
    {
        public string DisplayText { get; set; }

        public int Id { get; set; }

        public LanguageType Language { get; set; }

        public int ReferenceId { get; set; }
    }
}
