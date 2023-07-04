// <copyright file="EmailTemplateSendTryMailRequestExample.cs" company="KocSistem">
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
    public class EmailTemplateSendTryMailRequestExample : IExamplesProvider<SendEmailRequest>
    {
        public SendEmailRequest GetExamples()
        {
            return new SendEmailRequest
            {
                To = "test@test.com",
                Subject = "Test",
                Content = "Test"
            };
        }
    }
}