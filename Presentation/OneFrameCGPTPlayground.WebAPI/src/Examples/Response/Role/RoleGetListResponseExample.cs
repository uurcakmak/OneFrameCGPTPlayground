// <copyright file="RoleGetListResponseExample.cs" company="KocSistem">
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
    internal class RoleGetListResponseExample : IExamplesProvider<ServiceResponse<PagedResult<RoleGetResponse>>>
    {
        public ServiceResponse<PagedResult<RoleGetResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<RoleGetResponse>>(new PagedResult<RoleGetResponse>
            {
                Items = new List<RoleGetResponse>
                {
                    new RoleGetResponse
                    {
                        Name = "Role Name",
                        Description = "Role Description",
                        DisplayText = "Role Display Text",
                        Id = Guid.NewGuid().ToString(),
                    },
                },
                PageIndex = 0,
                PageSize = 10,
                TotalCount = 100,
                TotalPages = 10,
            });
        }
    }
}