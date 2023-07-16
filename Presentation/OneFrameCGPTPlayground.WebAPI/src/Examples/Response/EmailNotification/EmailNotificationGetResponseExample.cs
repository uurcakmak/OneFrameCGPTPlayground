// <copyright file="EmailNotificationGetResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    public class EmailNotificationGetResponseExample : IExamplesProvider<ServiceResponse<PagedResult<EmailNotificationResponse>>>
    {
        public ServiceResponse<PagedResult<EmailNotificationResponse>> GetExamples()
        {
            var list = new PagedResult<EmailNotificationResponse>
            {
                Items = new List<EmailNotificationResponse>
                {
                    new EmailNotificationResponse
                    {
                        Id = Guid.NewGuid(),
                        Bcc = "Bcc 1",
                        To = "To 1",
                        Cc = "Cc 1",
                        Subject = "Subject 1",
                        Body = "Body 1",
                        IsSent = true,
                        RetryCount = 1,
                        From = "From 1",
                        SentDate = DateTime.UtcNow,
                        InsertedDate = DateTime.UtcNow,
                    },
                    new EmailNotificationResponse
                    {
                        Id = Guid.NewGuid(),
                        Bcc = "Bcc 2",
                        To = "To 2",
                        Cc = "Cc 2",
                        Subject = "Subject 2",
                        Body = "Body 2",
                        IsSent = false,
                        RetryCount = 1,
                        From = "From 2",
                        SentDate = DateTime.UtcNow,
                        InsertedDate = DateTime.UtcNow,
                    },
                },
                PageIndex = 0,
                PageSize = 10,
                TotalCount = 100,
                TotalPages = 10,
            };

            return new ServiceResponse<PagedResult<EmailNotificationResponse>>(list);
        }
    }
}
