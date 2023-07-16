// <copyright file="EmailTemplateGetResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class EmailTemplateGetResponseExample : IExamplesProvider<ServiceResponse<PagedResult<EmailTemplateGetResponse>>>
    {
        public ServiceResponse<PagedResult<EmailTemplateGetResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<EmailTemplateGetResponse>>(new PagedResult<EmailTemplateGetResponse>
            {
                Items = new List<EmailTemplateGetResponse>
                {
                    new EmailTemplateGetResponse
                    {
                        Id = Guid.NewGuid(),
                        EmailContent = "EmailContent",
                        Subject = "Subject",
                        Translations = new List<Application.Abstractions.EmailTemplateTranslation.Contracts.EmailTemplateTranslationDto>
                        {
                                new Application.Abstractions.EmailTemplateTranslation.Contracts.EmailTemplateTranslationDto
                                {
                                   Id = Guid.NewGuid(),
                                   Subject = "Subject",
                                   EmailContent = "EmailContent",
                                   ReferenceId = Guid.NewGuid(),
                                   Language = Common.Enums.LanguageType.en,
                                }
                        }
                    }
                },
                TotalCount = 100,
                TotalPages = 10,
                PageIndex = 0,
                PageSize = 10,
            });
        }
    }
}