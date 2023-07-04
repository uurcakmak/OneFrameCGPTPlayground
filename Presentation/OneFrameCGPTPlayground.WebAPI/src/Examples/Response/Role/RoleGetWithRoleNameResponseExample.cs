// <copyright file="RoleGetWithRoleNameResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Enums;
using OneFrameCGPTPlayground.WebAPI.Model.Role;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleGetWithRoleNameResponseExample : IExamplesProvider<ServiceResponse<RoleGetWithTranslatesResponse>>
    {
        public ServiceResponse<RoleGetWithTranslatesResponse> GetExamples()
        {
            return new ServiceResponse<RoleGetWithTranslatesResponse>(new RoleGetWithTranslatesResponse
            {
                Name = "Admin",
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
            });
        }
    }
}