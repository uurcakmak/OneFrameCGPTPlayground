// <copyright file="LanguageProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Data.Relational;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts;

namespace OneFrameCGPTPlayground.Application.Language.Mappings
{
    /// <summary>
    ///  Definition Language Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            this.CreateMap<Domain.Language, LanguageDto>().ReverseMap();

            this.CreateMap<IPagedList<Domain.Language>, PagedResultDto<LanguageDto>>().ReverseMap();
        }
    }
}
