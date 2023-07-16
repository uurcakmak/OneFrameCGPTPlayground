// <copyright file="ApplicationSettingSearchResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSetting;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class ApplicationSettingSearchResponseExample : IExamplesProvider<ServiceResponse<PagedResult<ApplicationSettingResponse>>>
    {
        public ServiceResponse<PagedResult<ApplicationSettingResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<ApplicationSettingResponse>>(new PagedResult<ApplicationSettingResponse>
            {
                Items = new List<ApplicationSettingResponse>
                {
                    new ApplicationSettingResponse
                    {
                        Id = Guid.NewGuid(),
                        Key = "Key",
                        Value = "Value",
                        ValueType = "Value Type",
                        IsStatic = false,
                        CategoryId = Guid.NewGuid(),
                        CategoryName = "Category Name",
                        Status = "Status",
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