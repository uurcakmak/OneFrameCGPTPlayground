﻿// <copyright file="ApiMenuTreeViewItem.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Menu
{
    public class ApiMenuTreeViewItem
    {
        public ApiMenuTreeViewItem()
        {
            Children = new List<ApiMenuTreeViewItem>();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public int OrderId { get; set; }

        public ApiMenuTreeViewItemStateInfo State { get; set; }

        public virtual List<ApiMenuTreeViewItem> Children { get; set; }
    }
}
