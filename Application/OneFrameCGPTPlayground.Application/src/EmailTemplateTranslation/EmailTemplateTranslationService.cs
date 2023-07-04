// <copyright file="EmailTemplateTranslationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using System;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.Application.EmailTemplateTranslation
{
    /// <summary>
    /// Email Template Translation Service.
    /// </summary>
    /// <seealso cref="ApplicationCrudServiceAsync{EmailTemplateTranslation, EmailTemplateTranslationDto, Guid}" />
    /// <seealso cref="Abstractions.RoleTranslation.IRoleTranslationService" />
    [ExcludeFromCodeCoverage]
    public class EmailTemplateTranslationService : ApplicationCrudServiceAsync<Domain.EmailTemplateTranslation, EmailTemplateTranslationDto, Guid>, IEmailTemplateTranslationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplateTranslationService"/> class.
        /// </summary>
        /// <param name="emailTemplateTranslationRepository">The application role translation repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="dataManager">The data manager.</param>
        public EmailTemplateTranslationService(IRepository<Domain.EmailTemplateTranslation> emailTemplateTranslationRepository, IMapper mapper, IDataManager dataManager)
            : base(emailTemplateTranslationRepository, mapper, dataManager)
        {
        }
    }
}