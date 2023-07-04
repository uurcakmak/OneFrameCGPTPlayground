// <copyright file="ILoginAuditLogService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Excel.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.PdfExport.Contracts;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions
{
    public interface ILoginAuditLogService : IApplicationCrudServiceAsync<Domain.LoginAuditLog, LoginAuditLogDto, Guid>, IApplicationService
    {
        Task<ServiceResponse<PagedResultDto<LoginAuditLogDto>>> GetLoginAuditLogsAsync(PagedRequestDto pagedRequest);

        Task<ServiceResponse<PagedResultDto<LoginAuditLogDto>>> SearchAsync(LoginAuditLogSearchDto searchRequest);

        Task<ServiceResponse<ExcelExportDto>> SearchForExcelExportAsync(LoginAuditLogFilterDto searchRequest);

        Task<ServiceResponse<List<LoginAuditLogDto>>> SearchLoginLogsAsync(LoginAuditLogFilterDto searchRequest);

        Task<ServiceResponse<PdfExportDto>> SearchForPdfExportAsync(LoginAuditLogFilterDto searchRequest);
    }
}
