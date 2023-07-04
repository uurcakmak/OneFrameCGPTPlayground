// <copyright file="ServiceResponse500Example.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Models;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class ServiceResponse500Example : IExamplesProvider<ServiceResponse>
    {
        public ServiceResponse GetExamples()
        {
            return new ServiceResponse
            {
                IsSuccessful = false,
                Error = new ErrorInfo
                {
                    Code = StatusCodes.Status500InternalServerError,
                    CorrelationId = Guid.NewGuid(),
                    Details = "Detail information",
                    Message = "Error message",
                    ValidationErrors = null,
                },
            };
        }
    }
}