// <copyright file="Menu.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Domain.TranslationBase;
using KocSistem.OneFrame.Data.Relational;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Domain
{
    /// <summary>
    /// Menu.
    /// </summary>
    /// <seealso cref="MenuTranslation" />
    /// <seealso cref="int" />
    public class Menu : IMainTableTranslation<MenuTranslation>, IEntity<int>
    {
        public virtual ICollection<Menu> ChildMenu { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public string Icon { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        public int? ParentId { get; set; }

        public virtual Menu ParentMenu { get; set; }

        public List<MenuTranslation> Translations { get; set; }

        public string Url { get; set; }
    }
}