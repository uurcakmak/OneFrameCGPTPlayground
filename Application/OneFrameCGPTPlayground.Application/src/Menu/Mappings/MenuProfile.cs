// <copyright file="MenuProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;

namespace OneFrameCGPTPlayground.Application.Menu.Mappings
{
    /// <summary>
    /// Definition Menu Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            _ = CreateMap<Domain.Menu, MenuDto>().ReverseMap();

            _ = CreateMap<Domain.Menu, MenuDto>().ForTranslateMember(dest => dest.DisplayText);

            _ = CreateMap<MenuDto, UserMenuDto>().ReverseMap();
        }
    }
}
