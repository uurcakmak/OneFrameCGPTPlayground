﻿// <copyright file="RoleSearchRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.Role;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleSearchRequestExample : IExamplesProvider<RoleSearchRequest>
    {
        public RoleSearchRequest GetExamples()
        {
            return new RoleSearchRequest
            {
                PageIndex = 0,
                PageSize = 10,
                Name = "Search Value",
                Orders = new List<PagedRequestOrder>
               {
                   new PagedRequestOrder
                   {
                       ColumnName = "Column Name",
                       DirectionDesc = true,
                   },
               },
            };
        }
    }
}