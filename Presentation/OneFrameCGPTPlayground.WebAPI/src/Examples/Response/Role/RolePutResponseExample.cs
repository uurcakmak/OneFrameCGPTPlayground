// <copyright file="RolePutResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Domain;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RolePutResponseExample : IExamplesProvider<ServiceResponse<ApplicationRole>>
    {
        public ServiceResponse<ApplicationRole> GetExamples()
        {
            var id = Guid.NewGuid();
            return new ServiceResponse<ApplicationRole>(new ApplicationRole
            {
                Id = id,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN",
                Translations = new List<ApplicationRoleTranslation>
                {
                    new ApplicationRoleTranslation
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = id,
                        DisplayText = "Admin",
                        Language = Common.Enums.LanguageType.en,
                        Description = "Admin Description",
                    },
                    new ApplicationRoleTranslation
                    {
                        Id = Guid.NewGuid(),
                        ReferenceId = id,
                        DisplayText = "Yönetici",
                        Language = Common.Enums.LanguageType.en,
                        Description = "Yönetici Açıklaması",
                    },
                },
            });
        }
    }
}