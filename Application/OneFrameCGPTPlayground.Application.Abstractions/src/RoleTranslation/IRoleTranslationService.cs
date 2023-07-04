// <copyright file="IRoleTranslationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation.Contracts;
using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation
{
    /// <summary>
    /// IRoleService.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IRoleTranslationService : IApplicationCrudServiceAsync<ApplicationRoleTranslation, ApplicationRoleTranslationDto, Guid>, IApplicationService
    {
    }
}
