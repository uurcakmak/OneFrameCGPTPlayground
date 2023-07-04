// <copyright file="EmailTemplateListResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using Swashbuckle.AspNetCore.Filters;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    public class EmailTemplateListResponseExample : IExamplesProvider<List<EmailTemplateListResponse>>
    {
        public List<EmailTemplateListResponse> GetExamples()
        {
            var list = new List<EmailTemplateListResponse>
            {
                new EmailTemplateListResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "ForgotPassword",
                    SupportedLanguages = "ar, en, tr",
                    UpdatedDate = DateTime.UtcNow
                },
                new EmailTemplateListResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Welcome",
                    SupportedLanguages = "en, tr",
                    UpdatedDate = DateTime.UtcNow
                },
            };

            return list;
        }
    }
}