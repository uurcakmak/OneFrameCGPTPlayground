// <copyright file="ConfigurationGetDatabaseValuesResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class ConfigurationGetDatabaseValuesResponseExample : IExamplesProvider<ServiceResponse<Dictionary<string, object>>>
    {
        public ServiceResponse<Dictionary<string, object>> GetExamples()
        {
            var data = new Dictionary<string, object>
            {
                { "key01", "value01" },
                { "key02", 2 },
                { "key03", true },
                { "key04", 1.23M },
            };

            return new ServiceResponse<Dictionary<string, object>>(data);
        }
    }
}