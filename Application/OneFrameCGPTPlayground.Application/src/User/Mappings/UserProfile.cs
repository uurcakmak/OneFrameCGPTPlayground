// <copyright file="UserProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Domain;

namespace OneFrameCGPTPlayground.Application.User.Mappings
{
    /// <summary>
    ///  Definition User Entity AutoMapper Profiles.
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
            _ = CreateMap<ApplicationUser, UserDto>().ReverseMap();
            _ = CreateMap<ApplicationUser, UserRoleInfoDto>().ReverseMap();
        }
    }
}
