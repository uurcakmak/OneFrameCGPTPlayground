// <copyright file="RoleSaveRoleClaimsRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Role;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleSaveRoleClaimsRequestExample : IExamplesProvider<SaveRoleClaimsModel>
    {
        public SaveRoleClaimsModel GetExamples()
        {
            return new SaveRoleClaimsModel
            {
                Name = "Role Name",
                SelectedRoleClaimList = new List<string>()
                {
                    "Claim 1",
                    "Claim 2",
                    "Claim 3",
                },
            };
        }
    }
}