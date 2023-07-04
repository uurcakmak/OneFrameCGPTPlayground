// <copyright file="MenuProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts;
using OneFrameCGPTPlayground.WebAPI.Model.Menu;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    /// <summary>
    ///  Definition Menu Dto AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class MenuProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuProfile"/> class.
        /// </summary>
        public MenuProfile()
        {
            _ = CreateMap<MenuDto, MenuResponse>().ReverseMap();
            _ = CreateMap<UserMenuDto, MenuResponse>().ReverseMap();
            _ = CreateMap<MenuTreeViewItemDto, MenuTreeViewItem>().ReverseMap();
            _ = CreateMap<MenuTreeViewItemStateInfoDto, MenuTreeViewItemStateInfo>().ReverseMap();
            _ = CreateMap<SaveMenuOrderModel, SaveMenuOrderDto>().ReverseMap();
            _ = CreateMap<SaveMenuOrderItemModel, SaveMenuOrderItemDto>().ReverseMap();
        }
    }
}
