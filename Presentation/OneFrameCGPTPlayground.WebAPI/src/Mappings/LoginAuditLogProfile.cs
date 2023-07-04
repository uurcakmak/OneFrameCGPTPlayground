// <copyright file="LoginAuditLogProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;
using OneFrameCGPTPlayground.WebAPI.Model.LoginAuditLog;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    /// <summary>
    ///  Definition LoginAuditLog Dto AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class LoginAuditLogProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginAuditLogProfile"/> class.
        /// </summary>
        public LoginAuditLogProfile()
        {
            _ = CreateMap<LoginAuditLogDto, LoginAuditLogResponse>().ToTimeZone(x => x.InsertedDate).ReverseMap();
            _ = CreateMap<PagedResultDto<LoginAuditLogDto>, PagedResult<LoginAuditLogResponse>>().ReverseMap();
            _ = CreateMap<LoginAuditLogSearchRequest, LoginAuditLogSearchDto>().ReverseMap();
            _ = CreateMap<LoginAuditLogFilterRequest, LoginAuditLogFilterDto>().ToUtc(dest => dest.StartDate).ToUtc(dest => dest.EndDate).ReverseMap();
        }
    }
}