﻿// <copyright file="AccountRefreshResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountRefreshResponseExample : IExamplesProvider<ServiceResponse<LoginResponse>>
    {
        public ServiceResponse<LoginResponse> GetExamples()
        {
            var claims = new List<ClaimResponse>
            {
               new ClaimResponse { Name = "Claim Name", Value = "Claim Value" },
            };

            var result = new LoginResponse() { Token = Guid.NewGuid().ToString(), Claims = claims, RefreshToken = Guid.NewGuid().ToString() };

            return new ServiceResponse<LoginResponse>(result);
        }
    }
}