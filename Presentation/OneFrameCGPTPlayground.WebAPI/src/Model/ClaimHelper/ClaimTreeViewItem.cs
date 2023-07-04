// <copyright file="ClaimTreeViewItem.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper
{
    public class ClaimTreeViewItem
    {
        public ClaimTreeViewItem()
        {
            Children = new List<ClaimTreeViewItem>();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public ClaimTreeViewItemStateInfo State { get; set; }

        public virtual List<ClaimTreeViewItem> Children { get; set; }
    }
}