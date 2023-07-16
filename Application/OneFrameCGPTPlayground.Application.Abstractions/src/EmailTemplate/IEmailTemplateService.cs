// <copyright file="IEmailTemplateService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts;
using System;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate
{
    public interface IEmailTemplateService : IApplicationCrudServiceAsync<Domain.EmailTemplate, EmailTemplateDto, Guid>, IApplicationService
    {
        Task<ServiceResponse<PagedResultDto<EmailTemplateDto>>> GetEmailTemplateListAsync(PagedRequestDto pagedRequest);

        Task<ServiceResponse<EmailTemplateDto>> UpdateEmailTemplateAsync(EmailTemplateDto emailTemplateDto);

        Task<ServiceResponse<EmailTemplateDto>> GetEmailTemplateByIdAsync(Guid id);

        Task<ServiceResponse<EmailTemplateDto>> GetEmailTemplateByNameAsync(string emailTemplateName);

        Task<ServiceResponse<PagedResultDto<EmailTemplateDto>>> SearchAsync(EmailTemplateSearchDto emailTemplateGetRequest);
    }
}
