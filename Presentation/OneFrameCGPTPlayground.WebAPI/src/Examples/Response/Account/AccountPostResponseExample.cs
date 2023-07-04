// <copyright file="AccountPostResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountPostResponseExample : IExamplesProvider<ServiceResponse<CreatedAtRouteResult>>
    {
        public ServiceResponse<CreatedAtRouteResult> GetExamples()
        {
            return new ServiceResponse<CreatedAtRouteResult>(new CreatedAtRouteResult(
                "routeName",
                new { username = "ghostbusters@kocsistem.com.tr" },
                new { id = Guid.NewGuid().ToString(), username = "ghostbusters@kocsistem.com.tr" }));
        }
    }
}