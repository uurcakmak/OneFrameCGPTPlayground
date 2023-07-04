// <copyright file="ApiClaimTreeViewItem.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.ClaimHelper
{
    public class ApiClaimTreeViewItem
    {
        public ApiClaimTreeViewItem()
        {
            Children = new List<ApiClaimTreeViewItem>();
        }

        public virtual List<ApiClaimTreeViewItem> Children { get; set; }

        public string Id { get; set; }

        public ApiClaimTreeViewItemStateInfo State { get; set; }

        public string Text { get; set; }
    }
}
