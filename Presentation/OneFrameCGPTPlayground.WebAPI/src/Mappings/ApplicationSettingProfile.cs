// <copyright file="ApplicationSettingProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSetting;
using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSettingCategory;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    /// <summary>
    ///  Definition AppSetting Dto AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ApplicationSettingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingProfile"/> class.
        /// </summary>
        public ApplicationSettingProfile()
        {
            _ = CreateMap<ApplicationSettingDto, ApplicationSettingResponse>().ReverseMap();
            _ = CreateMap<PagedResultDto<ApplicationSettingDto>, PagedResult<ApplicationSettingResponse>>().ReverseMap();
            _ = CreateMap<ApplicationSettingDetailDto, ApplicationSettingResponse>().ReverseMap();
            _ = CreateMap<ApplicationSettingCategoryDto, ApplicationSettingCategoryResponse>().ReverseMap();
            _ = CreateMap<ApplicationSettingPutRequest, ApplicationSettingDto>().ReverseMap();
            _ = CreateMap<ApplicationSettingPostRequest, ApplicationSettingDto>().ReverseMap();
            _ = CreateMap<ApplicationSettingCategoryPutRequest, ApplicationSettingCategoryDto>().ReverseMap();
            _ = CreateMap<ApplicationSettingCategoryPostRequest, ApplicationSettingCategoryDto>().ReverseMap();
            _ = CreateMap<ApplicationSettingSearchRequest, ApplicationSettingSearchDto>().ReverseMap();
            _ = CreateMap<PagedResultDto<ApplicationSettingDetailDto>, PagedResult<ApplicationSettingResponse>>().ReverseMap();
            _ = CreateMap<ApplicationSettingCategorySearchRequest, ApplicationSettingCategorySearchDto>().ReverseMap();
            _ = CreateMap<PagedResultDto<ApplicationSettingCategoryDto>, PagedResult<ApplicationSettingCategoryResponse>>().ReverseMap();
        }
    }
}
