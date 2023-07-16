// <copyright file="RoleGetUsersInRoleResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleGetUsersInRoleResponseExample : IExamplesProvider<ServiceResponse<List<UserInRoleResponse>>>
    {
        public ServiceResponse<List<UserInRoleResponse>> GetExamples()
        {
            return new ServiceResponse<List<UserInRoleResponse>>(new List<UserInRoleResponse>
            {
                new UserInRoleResponse
                {
                    Id = Guid.NewGuid(),
                    Email = "ghostbusters@kocsistem.com.tr",
                    Username = "ghostbusters@kocsistem.com.tr",
                },
            });
        }
    }
}