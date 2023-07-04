// <copyright file="ApplicationSettingGetByKeyResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSetting;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class ApplicationSettingGetByKeyResponseExample : IExamplesProvider<ServiceResponse<ApplicationSettingResponse>>
    {
        public ServiceResponse<ApplicationSettingResponse> GetExamples()
        {
            return new ServiceResponse<ApplicationSettingResponse>(new ApplicationSettingResponse
            {
                Id = Guid.NewGuid(),
                Key = "Key",
                Value = "Value",
                ValueType = "Value Type",
                IsStatic = false,
                CategoryId = Guid.NewGuid(),
                CategoryName = "Category Name",
                Status = "Status",
            });
        }
    }
}