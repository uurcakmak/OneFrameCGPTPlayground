// <copyright file="EmailNotificationProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;

namespace OneFrameCGPTPlayground.WebAPI
{
    /// <summary>
    ///  Definition Email Dto AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class EmailNotificationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationProfile"/> class.
        /// </summary>
        public EmailNotificationProfile()
        {
            _ = CreateMap<EmailNotificationDto, EmailNotificationResponse>()
                .ToTimeZone(dest => dest.SentDate)
                .ToTimeZone(dest => dest.InsertedDate)
                .ReverseMap();

            _ = CreateMap<PagedResultDto<EmailNotificationDto>, PagedResult<EmailNotificationResponse>>().ReverseMap();
            _ = CreateMap<EmailNotificationSearchRequest, EmailNotificationSearchRequestDto>().ReverseMap();
        }
    }
}