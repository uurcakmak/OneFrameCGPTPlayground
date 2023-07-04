// <copyright file="EmailTemplateSearchResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class EmailTemplateSearchResponseExample : IExamplesProvider<ServiceResponse<PagedResult<EmailTemplateListResponse>>>
    {
        public ServiceResponse<PagedResult<EmailTemplateListResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<EmailTemplateListResponse>>(new PagedResult<EmailTemplateListResponse>
            {
                Items = new List<EmailTemplateListResponse>
                {
                    new EmailTemplateListResponse
                    {
                        Id = Guid.NewGuid(),
                    },
                },
                TotalCount = 100,
                TotalPages = 10,
                PageIndex = 0,
                PageSize = 10,
            });
        }
    }
}