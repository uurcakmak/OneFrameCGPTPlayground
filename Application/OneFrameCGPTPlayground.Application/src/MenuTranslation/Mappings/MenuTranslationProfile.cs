// <copyright file="MenuTranslationProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.MenuTranslation.Contracts;

namespace OneFrameCGPTPlayground.Application.MenuTranslation.Mappings
{
    /// <summary>
    /// Menu Translation Profile.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class MenuTranslationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuTranslationProfile"/> class.
        /// </summary>
        public MenuTranslationProfile()
        {
            _ = CreateMap<Domain.MenuTranslation, MenuTranslationDto>().ReverseMap();
        }
    }
}
