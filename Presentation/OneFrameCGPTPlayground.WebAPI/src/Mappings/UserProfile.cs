// <copyright file="UserProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory.Contract;
using OneFrameCGPTPlayground.Domain;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    /// <summary>
    ///  Definition ApplicationUser Entity And UserDto AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UserProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfile"/> class.
        /// </summary>
        public UserProfile()
        {
            _ = CreateMap<ApplicationUser, UserGetResponse>().ReverseMap();
            _ = CreateMap<UserDto, UserGetResponse>().ReverseMap();
            _ = CreateMap<IPagedList<ApplicationUser>, PagedResult<UserGetResponse>>().ReverseMap();
            _ = CreateMap<ApplicationUser, UserRoleInfoResponse>().ReverseMap();
            _ = CreateMap<UserDto, BasicUserInfoResponse>().ReverseMap();
            _ = CreateMap<UserDto, UserInRoleResponse>().ReverseMap();

            _ = CreateMap<UserConfirmationHistoryDto, ConfirmationCodeResponse>().ReverseMap();
            _ = CreateMap<UserConfirmationHistoryDto, ConfirmationCodeRequest>().ReverseMap();
        }
    }
}