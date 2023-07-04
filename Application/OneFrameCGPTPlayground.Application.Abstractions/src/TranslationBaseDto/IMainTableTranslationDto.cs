// <copyright file="IMainTableTranslationDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.TranslationBaseDto
{
    /// <summary>
    /// Main Table Translation Dto interface.
    /// </summary>
    /// <typeparam name="TTable">The type of the table.</typeparam>
    public interface IMainTableTranslationDto<TTable>
    {
        public List<TTable> Translations { get; set; }
    }
}
