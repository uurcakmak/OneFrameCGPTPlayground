// <copyright file="RolePutRequestExample.cs" company="KocSistem">
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
    internal class RolePutRequestExample : IExamplesProvider<RolePutRequest>
    {
        public RolePutRequest GetExamples()
        {
            return new RolePutRequest
            {
                UsersInRole = new List<string>()
                {
                    "Role 1",
                    "Role 2",
                },
                UsersNotInRole = new List<string>()
                {
                    "role 3",
                },
                Translations = new List<RoleTranslationsModel> { new RoleTranslationsModel { Description = "Admin Description", DisplayText = "Admin Display Text", Language = Common.Enums.LanguageType.en, }, },
            };
        }
    }
}