// <copyright file="RolePosteResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Enums;
using OneFrameCGPTPlayground.WebAPI.Model.Role;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RolePosteResponseExample : IExamplesProvider<ServiceResponse<CreatedAtRouteResult>>
    {
        public ServiceResponse<CreatedAtRouteResult> GetExamples()
        {
            return new ServiceResponse<CreatedAtRouteResult>(new CreatedAtRouteResult("routeName", new { roleName = "role name" }, new RolePostRequest
            {
                Name = "role name",
                Translations = new List<RoleTranslationsModel>()
                {
                    new RoleTranslationsModel
                    {
                        DisplayText = "Admin",
                        Language = LanguageType.en,
                        Description = "Admin Description",
                    },
                    new RoleTranslationsModel
                    {
                        DisplayText = "Yönetici",
                        Language = LanguageType.en,
                        Description = "Yönetici Açıklaması",
                    },
                },
            }));
        }
    }
}