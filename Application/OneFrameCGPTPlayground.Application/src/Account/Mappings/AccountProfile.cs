// <copyright file="AccountProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Data.Relational;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.Application.Account.Mappings
{
    /// <summary>
    ///  Definition User Entity AutoMapper Profiles.
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
            _ = CreateMap<IPagedList<ApplicationUser>, PagedResultDto<UserDto>>().ReverseMap();
            _ = CreateMap<ApplicationUser, UserDto>().ReverseMap();
            _ = CreateMap<Claim, ClaimDto>().ReverseMap();
        }
    }
}