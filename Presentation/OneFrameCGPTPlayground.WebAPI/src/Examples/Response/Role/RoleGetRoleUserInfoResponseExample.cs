// <copyright file="RoleGetRoleUserInfoResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.User;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleGetRoleUserInfoResponseExample : IExamplesProvider<ServiceResponse<List<UserRoleInfoResponse>>>
    {
        public ServiceResponse<List<UserRoleInfoResponse>> GetExamples()
        {
            return new ServiceResponse<List<UserRoleInfoResponse>>(new List<UserRoleInfoResponse>
            {
                new UserRoleInfoResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Ghost",
                    Surname = "Busters",
                    Email = "ghostbusters@kocsistem.com.tr",
                    Username = "ghostbusters@kocsistem.com.tr",
                    IsInRole = true,
                },
            });
        }
    }
}