// <copyright file="LoginAuditLogSearchResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.LoginAuditLog;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class LoginAuditLogSearchResponseExample : IExamplesProvider<ServiceResponse<PagedResult<LoginAuditLogResponse>>>
    {
        public ServiceResponse<PagedResult<LoginAuditLogResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<LoginAuditLogResponse>>(new PagedResult<LoginAuditLogResponse>
            {
                PageSize = 10,
                PageIndex = 0,
                TotalCount = 100,
                TotalPages = 10,
                Items = new List<LoginAuditLogResponse>
                {
                    new LoginAuditLogResponse
                    {
                        ApplicationUserName = "ApplicationUserName",
                        BrowserDetail = "BrowserDetail",
                        BrowserGuid = "BrowserGuid",
                        Hostname = "Hostname",
                        Id = Guid.NewGuid(),
                        InsertedDate = DateTime.UtcNow,
                        Ip = "127.0.0.1",
                        MacAddress = "MacAddress",
                        Message = "Message",
                        OsName = "OSName",
                        RequestHeaderInfo = "RequestHeaderInfo",
                        Success = false,
                    },
                },
            });
        }
    }
}