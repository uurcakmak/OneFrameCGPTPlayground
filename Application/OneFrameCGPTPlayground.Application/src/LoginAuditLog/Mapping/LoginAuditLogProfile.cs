// <copyright file="LoginAuditLogProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;
using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Application.LoginAuditLog.Mapping
{
    /// <summary>
    ///  Definition LoginAuditLog Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class LoginAuditLogProfile : Profile
    {
        public LoginAuditLogProfile()
        {
            _ = CreateMap<Domain.LoginAuditLog, LoginAuditLogDto>().ReverseMap();
            _ = CreateMap<IPagedList<Domain.LoginAuditLog>, PagedResultDto<LoginAuditLogDto>>().ReverseMap();
            _ = CreateMap<Domain.LoginAuditLog, LoginAuditLogExcelExportDto>().ToTimeZone(x => x.InsertedDate).ReverseMap();
            _ = CreateMap<Domain.LoginAuditLog, LoginAuditLogPdfExport>().ToTimeZone(x => x.InsertedDate).ReverseMap();
        }
    }
}
