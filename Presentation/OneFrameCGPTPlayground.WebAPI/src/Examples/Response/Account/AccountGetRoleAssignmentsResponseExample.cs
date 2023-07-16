// <copyright file="AccountGetRoleAssignmentsResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountGetRoleAssignmentsResponseExample : IExamplesProvider<ServiceResponse<List<RoleAssignmentResponse>>>
    {
        public ServiceResponse<List<RoleAssignmentResponse>> GetExamples()
        {
            return new ServiceResponse<List<RoleAssignmentResponse>>(new List<RoleAssignmentResponse>
            {
               new RoleAssignmentResponse
               {
                  IsAssigned = true,
                  RoleName = "Role 1",
               },
               new RoleAssignmentResponse
               {
                   IsAssigned = false,
                   RoleName = "Role 2",
               },
            });
        }
    }
}