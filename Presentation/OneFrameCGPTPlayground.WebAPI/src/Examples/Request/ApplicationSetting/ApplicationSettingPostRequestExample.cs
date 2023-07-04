// <copyright file="ApplicationSettingPostRequestExample.cs" company="KocSistem">
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
    public class ApplicationSettingPostRequestExample : IExamplesProvider<ApplicationSettingPostRequest>
    {
        public ApplicationSettingPostRequest GetExamples()
        {
            return new ApplicationSettingPostRequest
            {
                Key = "Key",
                Value = "Value",
                ValueType = "Value Type",
                CategoryId = Guid.NewGuid(),
                IsStatic = true,
            };
        }
    }
}