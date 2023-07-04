// <copyright file="IMainTableTranslation.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Domain.TranslationBase
{
    public interface IMainTableTranslation<TTable>
    {
        public List<TTable> Translations { get; set; }
    }
}
