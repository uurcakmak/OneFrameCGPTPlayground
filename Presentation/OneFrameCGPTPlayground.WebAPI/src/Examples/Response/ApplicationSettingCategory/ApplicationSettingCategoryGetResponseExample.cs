﻿// <copyright file="ApplicationSettingCategoryGetResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSettingCategory;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class ApplicationSettingCategoryGetResponseExample : IExamplesProvider<ServiceResponse<PagedResult<ApplicationSettingCategoryResponse>>>
    {
        public ServiceResponse<PagedResult<ApplicationSettingCategoryResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<ApplicationSettingCategoryResponse>>(new PagedResult<ApplicationSettingCategoryResponse>
            {
                Items = new List<ApplicationSettingCategoryResponse>
                {
                    new ApplicationSettingCategoryResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "Claim Name 1",
                    },
                    new ApplicationSettingCategoryResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = "Claim Name 2",
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