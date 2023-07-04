// <copyright file="AccountProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.WebAPI.Model.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.User;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    /// <summary>
    ///  Definition Menu Dto AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AccountProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountProfile"/> class.
        /// </summary>
        public AccountProfile()
        {
            _ = CreateMap<PagedRequest, PagedRequestDto>().ReverseMap();
            _ = CreateMap<PagedRequestOrder, PagedRequestOrderDto>().ReverseMap();
            _ = CreateMap<ClaimDto, ClaimResponse>().ReverseMap();
            _ = CreateMap<RoleAssignmentDto, RoleAssignmentResponse>().ReverseMap();
            _ = CreateMap<ClaimTreeViewItemDto, ClaimTreeViewItem>().ReverseMap();
            _ = CreateMap<ClaimTreeViewItemStateInfoDto, ClaimTreeViewItemStateInfo>().ReverseMap();
            _ = CreateMap<LoginDto, LoginResponse>().ReverseMap();
            _ = CreateMap<UserPutRequest, UserUpdateDto>().ReverseMap();
            _ = CreateMap<UserRolePutRequest, UserRoleUpdateDto>().ReverseMap();
            _ = CreateMap<UserPutForUserProfileRequest, UserUpdateDto>().ReverseMap();
            _ = CreateMap<SaveUserClaimsModel, SaveUserClaimsDto>().ReverseMap();
            _ = CreateMap<UserSearchRequest, UserSearchDto>().ReverseMap();
            _ = CreateMap<LoginDto, LoginResponse>().ReverseMap();
            _ = CreateMap<ClaimDto, ClaimResponse>().ReverseMap();
            _ = CreateMap<UserRegisterRequest, UserDto>().ReverseMap();
            _ = CreateMap<UserPostRequest, UserDto>().ReverseMap();
            _ = CreateMap<UserDto, UserGetResponse>().ReverseMap();
            _ = CreateMap<PagedResultDto<UserDto>, PagedResult<UserGetResponse>>().ReverseMap();
            _ = CreateMap<ResetPasswordDto, ResetPasswordRequest>().ReverseMap();
            _ = CreateMap<LoginRequestDto, LoginRequest>().ReverseMap();
            _ = CreateMap<TwoFactorVerificationDto, TwoFactorVerificationRequest>().ReverseMap();
            _ = CreateMap<AuthenticatorResponseDto, AuthenticatorResponse>().ReverseMap();
            _ = CreateMap<RegisterDto, LoginResponse>().ReverseMap();
        }
    }
}