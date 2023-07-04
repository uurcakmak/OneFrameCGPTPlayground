// <copyright file="RoleSearchResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.Role;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleSearchResponseExample : IExamplesProvider<ServiceResponse<PagedResult<RoleGetResponse>>>
    {
        public ServiceResponse<PagedResult<RoleGetResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<RoleGetResponse>>(new PagedResult<RoleGetResponse>
            {
                PageSize = 10,
                PageIndex = 0,
                TotalCount = 100,
                TotalPages = 10,
                Items = new List<RoleGetResponse>
                {
                    new RoleGetResponse
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Admin",
                        Description = "Admin Description",
                        DisplayText = "Admin Display Text",
                    },
                },
            });
        }
    }
}