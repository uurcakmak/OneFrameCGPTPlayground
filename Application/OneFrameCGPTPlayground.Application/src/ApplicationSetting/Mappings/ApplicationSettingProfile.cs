// <copyright file="ApplicationSettingProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Application.ApplicationSetting.Mappings
{
    /// <summary>
    ///  Definition ApplicationSetting Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ApplicationSettingProfile : Profile
    {
        public ApplicationSettingProfile()
        {
            _ = CreateMap<Domain.ApplicationSetting, ApplicationSettingDto>().ReverseMap();
            _ = CreateMap<IPagedList<Domain.ApplicationSetting>, PagedResultDto<ApplicationSettingDto>>().ReverseMap();
            _ = CreateMap<Domain.ApplicationSetting, ApplicationSettingDetailDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            _ = CreateMap<IPagedList<Domain.ApplicationSetting>, PagedResultDto<ApplicationSettingDetailDto>>().ReverseMap();
        }
    }
}
