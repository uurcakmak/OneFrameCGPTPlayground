// <copyright file="RoleTranslationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation;
using OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation.Contracts;
using OneFrameCGPTPlayground.Domain;
using System;

namespace OneFrameCGPTPlayground.Application.RoleTranslation
{
    /// <summary>
    /// Role Translation Service.
    /// </summary>
    /// <seealso cref="IRoleTranslationService" />
    public class RoleTranslationService : ApplicationCrudServiceAsync<ApplicationRoleTranslation, ApplicationRoleTranslationDto, Guid>, IRoleTranslationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleTranslationService"/> class.
        /// </summary>
        /// <param name="applicationRoleTranslationRepository">The application role translation repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="dataManager">The data manager.</param>
        public RoleTranslationService(IRepository<ApplicationRoleTranslation> applicationRoleTranslationRepository, IMapper mapper, IDataManager dataManager)
            : base(applicationRoleTranslationRepository, mapper, dataManager)
        {
        }
    }
}