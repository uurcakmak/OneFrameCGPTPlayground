// <copyright file="IEmailTemplateTranslationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation
{
    /// <summary>
    /// IEmailTemplateService.
    /// </summary>
    /// <seealso cref="KocSistem.OneFrame.DesignObjects.IApplicationService" />
    public interface IEmailTemplateTranslationService : IApplicationCrudServiceAsync<Domain.EmailTemplateTranslation, EmailTemplateTranslationDto, Guid>, IApplicationService
    {
    }
}
