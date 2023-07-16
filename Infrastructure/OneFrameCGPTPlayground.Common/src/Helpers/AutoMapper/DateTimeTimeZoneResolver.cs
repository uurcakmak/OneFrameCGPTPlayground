// <copyright file="DateTimeTimeZoneResolver.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using Microsoft.Extensions.Options;
using OneFrameCGPTPlayground.Common.Extensions;
using System;

namespace OneFrameCGPTPlayground.Common.Helpers.AutoMapper
{
    public class DateTimeTimeZoneResolver<TSource, TDestination> : IMemberValueResolver<TSource, TDestination, DateTime?, DateTime?>
    {
        private readonly UserLocalizationSettings _userLocalizationSettings;

        public DateTimeTimeZoneResolver(IOptions<UserLocalizationSettings> userLocalizationSettings)
        {
            _userLocalizationSettings = userLocalizationSettings.Value;
        }

        public DateTime? Resolve(TSource source, TDestination destination, DateTime? sourceMember, DateTime? destMember, ResolutionContext context)
        {
            return sourceMember?.ToTimeZone(_userLocalizationSettings.TimeZone);
        }
    }
}