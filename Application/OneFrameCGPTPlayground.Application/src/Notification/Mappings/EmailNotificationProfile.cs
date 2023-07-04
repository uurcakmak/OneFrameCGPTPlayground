// <copyright file="EmailNotificationProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Application.ApplicationSetting.Mappings
{
    /// <summary>
    ///  Definition Email Notification Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class EmailNotificationProfile : Profile
    {
        public EmailNotificationProfile()
        {
            _ = CreateMap<Domain.EmailNotification, EmailNotificationDto>().ReverseMap();
            _ = CreateMap<IPagedList<Domain.EmailNotification>, PagedResultDto<EmailNotificationDto>>().ReverseMap();
        }
    }
}
