// <copyright file="LanguageProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;
using OneFrameCGPTPlayground.WebAPI.Model.Language;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    public class LanguageProfile : Profile
    {
        /// <summary>
        ///  Definition Language Dto AutoMapper Profiles.
        /// </summary>
        /// <seealso cref="Profile" />
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public LanguageProfile()
        {
            _ = CreateMap<LanguagePostRequest, LanguageDto>().ReverseMap();

            _ = CreateMap<LanguagePutRequest, LanguageDto>().ReverseMap();

            _ = CreateMap<LanguageDto, LanguageListResponse>().ToTimeZone(x => x.UpdatedDate).ReverseMap();

            _ = CreateMap<LanguageDto, LanguageGetByIdResponse>().ReverseMap();

            _ = CreateMap<PagedResultDto<LanguageDto>, PagedResult<LanguageListResponse>>().ReverseMap();

            _ = CreateMap<LanguageDto, LanguageResponse>().ReverseMap();

            _ = CreateMap<LanguageSearchRequest, LanguageSearchDto>().ReverseMap();

            _ = CreateMap<PagedResultDto<LanguageDto>, PagedResult<LanguageResponse>>().ReverseMap();
        }
    }
}
