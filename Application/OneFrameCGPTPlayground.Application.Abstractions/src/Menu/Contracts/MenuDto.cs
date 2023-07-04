// <copyright file="MenuDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts
{
    /// <summary>
    /// Menu Dto.
    /// </summary>
    public class MenuDto : IDto<int>
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public string DisplayText { get; set; }

        public string Icon { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        public int? ParentId { get; set; }

        public string Url { get; set; }
    }
}