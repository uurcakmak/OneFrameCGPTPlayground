// <copyright file="EmailTemplateSearchRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class EmailTemplateSearchRequestExample : IExamplesProvider<EmailTemplateSearchRequest>
    {
        public EmailTemplateSearchRequest GetExamples()
        {
            return new EmailTemplateSearchRequest
            {
                PageIndex = 0,
                PageSize = 10,
                Name = "search value",
                Orders = new List<PagedRequestOrder>
               {
                   new PagedRequestOrder
                   {
                       ColumnName = "columnName",
                       DirectionDesc = true,
                   },
               },
            };
        }
    }
}