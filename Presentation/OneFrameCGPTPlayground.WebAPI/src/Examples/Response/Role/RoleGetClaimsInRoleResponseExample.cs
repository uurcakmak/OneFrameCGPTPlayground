// <copyright file="RoleGetClaimsInRoleResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleGetClaimsInRoleResponseExample : IExamplesProvider<ServiceResponse<List<ClaimResponse>>>
    {
        public ServiceResponse<List<ClaimResponse>> GetExamples()
        {
            return new ServiceResponse<List<ClaimResponse>>(new List<ClaimResponse>
            {
               new ClaimResponse
               {
                   Name = "Claim Name",
                   Value = "Claim Value",
               },
            });
        }
    }
}