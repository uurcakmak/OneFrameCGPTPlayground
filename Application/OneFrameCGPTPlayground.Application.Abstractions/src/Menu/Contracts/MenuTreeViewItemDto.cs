// <copyright file="MenuTreeViewItemDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts
{
    public class MenuTreeViewItemDto
    {
        public MenuTreeViewItemDto()
        {
            Children = new List<MenuTreeViewItemDto>();
        }

        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Text { get; set; }

        public int OrderId { get; set; }

        public MenuTreeViewItemStateInfoDto State { get; set; }

        public List<MenuTreeViewItemDto> Children { get; set; }
    }
}
