// <copyright file="ITranslationTableDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Enums;
using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Application.Abstractions.TranslationBaseDto
{
    /// <summary>
    /// Translation Table Dto interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <seealso cref="IDto{TPrimaryKey}" />
    public interface ITranslationTableDto<TPrimaryKey> : IDto<TPrimaryKey>
    {
        public LanguageType Language { get; set; }

        public TPrimaryKey ReferenceId { get; set; }
    }
}
