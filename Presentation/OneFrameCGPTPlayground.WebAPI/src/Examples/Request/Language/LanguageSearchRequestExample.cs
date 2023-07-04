// <copyright file="LanguageSearchRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Language;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request.Language
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class LanguageSearchRequestExample : IExamplesProvider<LanguageSearchRequest>
    {
        public LanguageSearchRequest GetExamples()
        {
            return new LanguageSearchRequest
            {
                PageIndex = 0,
                PageSize = 10,
                Key = "Search Value",
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
