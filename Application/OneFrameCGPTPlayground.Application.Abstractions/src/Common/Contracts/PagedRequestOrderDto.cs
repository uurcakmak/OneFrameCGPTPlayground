// <copyright file="PagedRequestOrderDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts
{
    /// <summary>
    /// Paged RequestOrder Dto.
    /// </summary>
    public class PagedRequestOrderDto
    {
        public string ColumnName { get; set; }

        public bool DirectionDesc { get; set; }
    }
}
