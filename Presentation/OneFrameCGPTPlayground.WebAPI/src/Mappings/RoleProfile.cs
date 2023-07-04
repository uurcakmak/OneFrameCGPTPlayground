// <copyright file="RoleProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;
using OneFrameCGPTPlayground.Domain;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.Role;
using OneFrameCGPTPlayground.WebAPI.Model.User;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    /// <summary>
    ///  Definition ApplicationRole Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class RoleProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProfile"/> class.
        /// </summary>
        public RoleProfile()
        {
            _ = CreateMap<ApplicationRoleDto, RolePutResponse>().ReverseMap();
            _ = CreateMap<RolePutRequest, RoleUpdateDto>().ReverseMap();
            _ = CreateMap<ApplicationRoleDto, RoleGetResponse>().ReverseMap();
            _ = CreateMap<ApplicationRole, RolePutResponse>().ReverseMap();
            _ = CreateMap<RoleClaimPostRequest, RoleClaimDto>().ReverseMap();
            _ = CreateMap<ApplicationRoleDto, ApplicationRoleModel>().ReverseMap();
            _ = CreateMap<UserRoleInfoDto, UserRoleInfoResponse>().ReverseMap();
            _ = CreateMap<SaveRoleClaimsModel, SaveRoleClaimsDto>().ReverseMap();
            _ = CreateMap<RoleSearchRequest, RoleSearchDto>().ReverseMap();
            _ = CreateMap<PagedResultDto<ApplicationRoleDto>, PagedResult<RoleGetResponse>>().ReverseMap();

            _ = CreateMap<RolePostRequest, ApplicationRoleDto>().ReverseMap();
            _ = CreateMap<ApplicationRoleDto, RoleGetWithTranslatesResponse>().ReverseMap();
            _ = CreateMap<RoleTranslationsModel, ApplicationRoleTranslationDto>().ReverseMap();

            _ = CreateMap<ApplicationRoleDto, ApplicationRoleModel>()
                .ForTranslateMember(dest => dest.DisplayText)
                .ForTranslateMember(dest => dest.Description);

            _ = CreateMap<ApplicationRoleDto, RoleGetResponse>()
                .ForTranslateMember(dest => dest.DisplayText)
                .ForTranslateMember(dest => dest.Description);
        }
    }
}