// <copyright file="ApplicationSettingCategoryListResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSettingCategory;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class ApplicationSettingCategoryListResponseExample : IExamplesProvider<ServiceResponse<List<ApplicationSettingCategoryResponse>>>
    {
        public ServiceResponse<List<ApplicationSettingCategoryResponse>> GetExamples()
        {
            var list = new List<ApplicationSettingCategoryResponse>
            {
                new ApplicationSettingCategoryResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Category Name 1",
                },
                new ApplicationSettingCategoryResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Category Name 2",
                },
            };

            return new ServiceResponse<List<ApplicationSettingCategoryResponse>>(list);
        }
    }
}