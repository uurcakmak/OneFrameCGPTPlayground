// <copyright file="UserLocalizationSettings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Globalization;

namespace OneFrameCGPTPlayground.Common.Helpers.AutoMapper
{
    public class UserLocalizationSettings
    {
        public TimeZoneInfo TimeZone { get; set; }

        public CultureInfo CultureInfo { get; set; }
    }
}