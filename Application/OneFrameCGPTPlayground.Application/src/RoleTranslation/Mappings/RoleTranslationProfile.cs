﻿// <copyright file="RoleTranslationProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation.Contracts;
using OneFrameCGPTPlayground.Domain;

namespace OneFrameCGPTPlayground.Application.RoleTranslation.Mappings
{
    /// <summary>
    /// Role Translation Profile.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class RoleTranslationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleTranslationProfile"/> class.
        /// </summary>
        public RoleTranslationProfile()
        {
            _ = CreateMap<ApplicationRoleTranslation, ApplicationRoleTranslationDto>().ReverseMap();
        }
    }
}
