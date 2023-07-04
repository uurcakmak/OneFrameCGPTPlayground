// <copyright file="MenuTranslation.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Domain.TranslationBase;

namespace OneFrameCGPTPlayground.Domain
{
    /// <summary>
    /// /enuTranslation.
    /// </summary>
    /// <seealso cref="int" />
    public class MenuTranslation : ITranslationTable<Menu, int>
    {
        public string DisplayText { get; set; }

        public int Id { get; set; }

        public Common.Enums.LanguageType Language { get; set; }

        public Menu Reference { get; set; }

        public int ReferenceId { get; set; }
    }
}