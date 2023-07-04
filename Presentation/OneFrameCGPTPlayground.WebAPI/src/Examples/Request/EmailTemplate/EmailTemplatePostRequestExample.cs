// <copyright file="EmailTemplatePostRequestExample.cs" company="KocSistem">
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
    public class EmailTemplatePostRequestExample : IExamplesProvider<EmailTemplatePostRequest>
    {
        public EmailTemplatePostRequest GetExamples()
        {
            return new EmailTemplatePostRequest
            {
                Name = "ForgotPassword",
            };
        }
    }
}
