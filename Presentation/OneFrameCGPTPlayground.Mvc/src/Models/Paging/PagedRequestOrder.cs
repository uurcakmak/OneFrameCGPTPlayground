// <copyright file="PagedRequestOrder.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Paging
{
    public class PagedRequestOrder
    {
        public string ColumnName { get; set; }

        public bool DirectionDesc { get; set; }
    }
}
