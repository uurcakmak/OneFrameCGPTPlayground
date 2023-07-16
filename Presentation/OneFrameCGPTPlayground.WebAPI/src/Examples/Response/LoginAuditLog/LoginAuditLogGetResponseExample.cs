// <copyright file="LoginAuditLogGetResponseExample.cs" company="KocSistem">
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
    public class LoginAuditLogGetResponseExample : IExamplesProvider<ServiceResponse<PagedResult<LoginAuditLogResponse>>>
    {
        public ServiceResponse<PagedResult<LoginAuditLogResponse>> GetExamples()
        {
            var list = new PagedResult<LoginAuditLogResponse>
            {
                Items = new List<LoginAuditLogResponse>
                {
                    new LoginAuditLogResponse
                    {
                        Id = Guid.NewGuid(),
                        Hostname = "Hostname 1",
                        Ip = "IP Address 1",
                        MacAddress = "MacAddress 1",
                        Message = string.Empty,
                        RequestHeaderInfo = "RequestHeaderInfo 1",
                        BrowserDetail = "BrowserDetail 1",
                        BrowserGuid = "BrowserGuid 1",
                    },
                    new LoginAuditLogResponse
                    {
                        Id = Guid.NewGuid(),
                        Hostname = "Hostname 2",
                        Ip = "IP Address 2",
                        MacAddress = "MacAddress 2",
                        Message = string.Empty,
                        RequestHeaderInfo = "RequestHeaderInfo 2",
                        BrowserDetail = "BrowserDetail 2",
                        BrowserGuid = "BrowserGuid 2",
                    },
                },
                PageIndex = 0,
                PageSize = 10,
                TotalCount = 100,
                TotalPages = 10,
            };

            return new ServiceResponse<PagedResult<LoginAuditLogResponse>>(list);
        }
    }
}