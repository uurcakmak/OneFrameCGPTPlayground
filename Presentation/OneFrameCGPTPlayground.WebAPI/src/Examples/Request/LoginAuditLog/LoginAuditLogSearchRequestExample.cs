// <copyright file="LoginAuditLogSearchRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.LoginAuditLog;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class LoginAuditLogSearchRequestExample : IExamplesProvider<LoginAuditLogSearchRequest>
    {
        public LoginAuditLogSearchRequest GetExamples()
        {
            return new LoginAuditLogSearchRequest
            {
                PageIndex = 0,
                PageSize = 10,
                Value = "Search Value",
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