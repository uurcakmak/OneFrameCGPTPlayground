// <copyright file="EmailTemplateGetByIdResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class EmailTemplateGetByIdResponseExample : IExamplesProvider<ServiceResponse<EmailTemplateGetResponse>>
    {
        public ServiceResponse<EmailTemplateGetResponse> GetExamples()
        {
            return new ServiceResponse<EmailTemplateGetResponse>(new EmailTemplateGetResponse
            {
                Id = Guid.NewGuid(),
                Subject = "Subject",
                EmailContent = "EmailContent",
                Translations = new System.Collections.Generic.List<Application.Abstractions.EmailTemplateTranslation.Contracts.EmailTemplateTranslationDto>
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
            });
        }
    }
}