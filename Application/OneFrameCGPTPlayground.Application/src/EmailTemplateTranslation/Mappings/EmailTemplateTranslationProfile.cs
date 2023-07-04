// <copyright file="EmailTemplateTranslationProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;

namespace OneFrameCGPTPlayground.Application.EmailTemplateTranslation.Mappings
{
    /// <summary>
    /// Role Translation Profile.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class EmailTemplateTranslationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplateTranslationProfile"/> class.
        /// </summary>
        public EmailTemplateTranslationProfile()
        {
            this.CreateMap<Domain.EmailTemplateTranslation, EmailTemplateTranslationDto>().ReverseMap();
        }
    }
}
