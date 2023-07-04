// <copyright file="PagedResultDto{T}.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts
{
    /// <summary>
    /// PagedResultDto{T}.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class PagedResultDto<T>
    {
        public IList<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }
    }
}
