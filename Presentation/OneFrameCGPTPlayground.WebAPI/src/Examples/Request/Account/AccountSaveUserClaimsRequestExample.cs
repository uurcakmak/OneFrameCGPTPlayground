// <copyright file="AccountSaveUserClaimsRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountSaveUserClaimsRequestExample : IExamplesProvider<SaveUserClaimsModel>
    {
        public SaveUserClaimsModel GetExamples()
        {
            return new SaveUserClaimsModel
            {
                Name = "Role Name",
                SelectedUserClaimList = new List<string>()
                {
                    "Claim 1",
                    "Claim 2",
                    "Claim 3",
                },
            };
        }
    }
}