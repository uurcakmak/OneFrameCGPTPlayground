// <copyright file="ApplicationSettingCategoryProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Data.Relational;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;

namespace OneFrameCGPTPlayground.Application.ApplicationSettingCategory.Mapping
{
    /// <summary>
    ///  Definition ApplicationSettingCategory Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ApplicationSettingCategoryProfile : Profile
    {
        public ApplicationSettingCategoryProfile()
        {
            _ = CreateMap<Domain.ApplicationSettingCategory, ApplicationSettingCategoryDto>().ReverseMap();
            _ = CreateMap<IPagedList<Domain.ApplicationSettingCategory>, PagedResultDto<ApplicationSettingCategoryDto>>().ReverseMap();
        }
    }
}
