// <copyright file="ITranslationTable.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Domain.TranslationBase
{
    /// <summary>
    /// Translation Table interface.
    /// </summary>
    /// <typeparam name="TTable">The type of the table.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <seealso cref="IEntity{TPrimaryKey}" />
    public interface ITranslationTable<TTable, TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public Common.Enums.LanguageType Language { get; set; }

        public TTable Reference { get; set; }

        public TPrimaryKey ReferenceId { get; set; }
    }
}
