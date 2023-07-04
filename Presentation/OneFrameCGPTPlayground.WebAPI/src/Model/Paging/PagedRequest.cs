// <copyright file="PagedRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.Paging
{
    public class PagedRequest
    {
        public PagedRequest()
        {
            Orders = new List<PagedRequestOrder>();
        }

        public List<PagedRequestOrder> Orders { get; set; }

        [Range(0, int.MaxValue)]
        public int PageIndex { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
    }
}
