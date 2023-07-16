// <copyright file="RoleGetResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.Role;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleGetResponseExample : IExamplesProvider<ServiceResponse<List<ApplicationRoleModel>>>
    {
        public ServiceResponse<List<ApplicationRoleModel>> GetExamples()
        {
            var id = Guid.NewGuid();
            return new ServiceResponse<List<ApplicationRoleModel>>(new List<ApplicationRoleModel>
            {
                new ApplicationRoleModel
                {
                    Id = id,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Description = "Admin Description",
                    DisplayText = "Admin Display Text",
                },
            });
        }
    }
}