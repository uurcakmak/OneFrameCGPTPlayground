// <copyright file="EmailTemplatePutResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class EmailTemplatePutResponseExample : IExamplesProvider<ServiceResponse<EmailTemplateGetResponse>>
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
                        Subject = "Subject",
                        EmailContent = "EmailContent",
                        Language = Common.Enums.LanguageType.en,
                     }
                }
            });
        }
    }
}