// <copyright file="PagedRequestOrder.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.Paging
{
    public class PagedRequestOrder
    {
        [Required]
        public string ColumnName { get; set; }

        [Required]
        public bool DirectionDesc { get; set; }
    }
}
