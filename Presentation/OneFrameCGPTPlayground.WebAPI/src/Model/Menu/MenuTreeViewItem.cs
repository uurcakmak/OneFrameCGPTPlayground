// <copyright file="MenuTreeViewItem.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.Menu
{
    public class MenuTreeViewItem
    {
        public MenuTreeViewItem()
        {
            Children = new List<MenuTreeViewItem>();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public int OrderId { get; set; }

        public MenuTreeViewItemStateInfo State { get; set; }

        public List<MenuTreeViewItem> Children { get; set; }
    }
}
