// <copyright file="ApplicationSettingPutRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSetting;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class ApplicationSettingPutRequestExample : IExamplesProvider<ApplicationSettingPutRequest>
    {
        public ApplicationSettingPutRequest GetExamples()
        {
            return new ApplicationSettingPutRequest
            {
                Key = "Key",
                Value = "Value",
                ValueType = "Value Type",
                CategoryId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                IsStatic = true,
            };
        }
    }
}