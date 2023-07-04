// <copyright file="EmailTemplatePutRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class EmailTemplatePutRequestExample : IExamplesProvider<EmailTemplatePutRequest>
    {
        public EmailTemplatePutRequest GetExamples()
        {
            return new EmailTemplatePutRequest
            {
                Id = Guid.NewGuid(),
                To = "test@test.com",
                Bcc = "test@test.com",
                Cc = "test@test.com",
                Translations = new System.Collections.Generic.List<EmailTemplateTranslationsModel>()
                {
                     new EmailTemplateTranslationsModel
                     {
                        Subject = "Subject",
                        EmailContent = "EmailContent",
                        Language = Common.Enums.LanguageType.en,
                     }
                }
            };
        }
    }
}