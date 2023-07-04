﻿// <copyright file="ServiceResponse204Example.cs" company="KocSistem">
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
    internal class ServiceResponse204Example : IExamplesProvider<ServiceResponse<string>>
    {
        public ServiceResponse<string> GetExamples() => new (
                "Not found.",
                new ErrorInfo
                {
                    Code = StatusCodes.Status204NoContent,
                    CorrelationId = Guid.NewGuid(),
                    Details = "Detail information",
                    Message = "Error message",
                    ValidationErrors = null,
                });
    }
}