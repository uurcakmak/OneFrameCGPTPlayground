// <copyright file="PagedRequestDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts
{
    /// <summary>
    /// Paged Request Dto.
    /// </summary>
    public class PagedRequestDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedRequestDto"/> class.
        /// </summary>
        public PagedRequestDto()
        {
            Orders = new List<PagedRequestOrderDto>();
        }

        public List<PagedRequestOrderDto> Orders { get; set; }

        [Range(0, int.MaxValue)]
        public int PageIndex { get; set; }

        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }
    }
}
