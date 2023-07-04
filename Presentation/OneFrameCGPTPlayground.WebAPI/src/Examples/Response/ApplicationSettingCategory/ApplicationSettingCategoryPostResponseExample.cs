// <copyright file="ApplicationSettingCategoryPostResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSettingCategory;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class ApplicationSettingCategoryPostResponseExample : IExamplesProvider<ServiceResponse<ApplicationSettingCategoryResponse>>
    {
        public ServiceResponse<ApplicationSettingCategoryResponse> GetExamples()
        {
            return new ServiceResponse<ApplicationSettingCategoryResponse>(new ApplicationSettingCategoryResponse
            {
                Id = Guid.NewGuid(),
                Name = "Category Name",
            });
        }
    }
}