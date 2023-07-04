// <copyright file="RoleProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts;
using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Application.Role.Mappings
{
    /// <summary>
    ///  Definition User Entity AutoMapper Profiles.
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
            _ = CreateMap<ApplicationRole, ApplicationRoleDto>().ReverseMap();
            _ = CreateMap<IPagedList<ApplicationRole>, PagedResultDto<ApplicationRoleDto>>().ReverseMap();
        }
    }
}
