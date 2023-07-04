// <copyright file="DateTimeExtensions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Globalization;
using TimeZoneConverter;

namespace OneFrameCGPTPlayground.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromTimeZoneToUtc(this DateTime timeZoneDate, TimeZoneInfo timeZone)
        {
            timeZoneDate = DateTime.SpecifyKind(timeZoneDate, DateTimeKind.Unspecified);
            var utc = TimeZoneInfo.ConvertTimeToUtc(timeZoneDate, timeZone);

            return utc;
        }

        public static DateTime FromTimeZoneToUtc(this DateTime timeZoneDate, string timeZoneId)
        {
            var timeZone = GetTimeZoneInfo(timeZoneId);

            var utc = timeZoneDate.FromTimeZoneToUtc(timeZone);

            return utc;
        }

        public static string FromTimeZoneToUtcWithCulture(this DateTime timeZoneDate, TimeZoneInfo timeZone, CultureInfo culture = null)
        {
            culture ??= CultureInfo.DefaultThreadCurrentCulture ?? new CultureInfo("en-US");

            var utc = timeZoneDate.FromTimeZoneToUtc(timeZone);

            return utc.ToCultureFormat(culture);
        }

        public static string FromTimeZoneToUtcWithCulture(this DateTime timeZoneDate, TimeZoneInfo timeZone, string culture = null)
        {
            CultureInfo cultureInfo;
            if (string.IsNullOrEmpty(culture))
            {
                cultureInfo = CultureInfo.DefaultThreadCurrentCulture ?? new CultureInfo("en-US");
            }
            else
            {
                cultureInfo = new CultureInfo(culture);
            }

            var utc = timeZoneDate.FromTimeZoneToUtcWithCulture(timeZone, cultureInfo);

            return utc;
        }

        public static string FromTimeZoneToUtcWithCulture(this DateTime timeZoneDate, string timeZoneId, string culture = null)
        {
            var timeZone = GetTimeZoneInfo(timeZoneId);

            var utc = timeZoneDate.FromTimeZoneToUtcWithCulture(timeZone, culture);

            return utc;
        }

        public static TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
        {
            if (TZConvert.TryGetTimeZoneInfo(timeZoneId, out var timeZone))
            {
                return timeZone;
            }

            timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            if (!TimeZoneInfo.TryConvertWindowsIdToIanaId(timeZone.Id, out var ianaTimezoneId))
            {
                return null;
            }

            timeZone = TimeZoneInfo.FindSystemTimeZoneById(ianaTimezoneId);

            return timeZone;
        }

        public static string ToCultureFormat(this DateTime time, CultureInfo culture = null)
        {
            if (culture == null)
            {
                culture = CultureInfo.DefaultThreadCurrentCulture ?? new CultureInfo("en-US");
            }

            return time.ToString(culture);
        }

        public static DateTime ToTimeZone(this DateTime utc, string timeZoneId)
        {
            var timeZone = GetTimeZoneInfo(timeZoneId);

            var convertedTime = utc.ToTimeZone(timeZone);

            return convertedTime;
        }

        public static DateTime ToTimeZone(this DateTime utc, TimeZoneInfo timeZone)
        {
            var convertedTime = TimeZoneInfo.ConvertTimeFromUtc(utc, timeZone);

            return convertedTime;
        }

        public static string ToTimeZoneWithCulture(this DateTime utc, TimeZoneInfo timeZone, CultureInfo culture = null)
        {
            if (culture == null)
            {
                culture = CultureInfo.DefaultThreadCurrentCulture ?? new CultureInfo("en-US");
            }

            var convertedTime = utc.ToTimeZone(timeZone);

            return convertedTime.ToCultureFormat(culture);
        }

        public static string ToTimeZoneWithCulture(this DateTime utc, TimeZoneInfo timeZone, string culture = null)
        {
            CultureInfo cultureInfo;
            if (string.IsNullOrEmpty(culture))
            {
                cultureInfo = CultureInfo.DefaultThreadCurrentCulture ?? new CultureInfo("en-US");
            }
            else
            {
                cultureInfo = new CultureInfo(culture);
            }

            var convertedTime = utc.ToTimeZoneWithCulture(timeZone, cultureInfo);

            return convertedTime;
        }

        public static string ToTimeZoneWithCulture(this DateTime utc, string timeZoneId, string culture = null)
        {
            var timeZone = GetTimeZoneInfo(timeZoneId);

            var convertedTime = utc.ToTimeZoneWithCulture(timeZone, culture);

            return convertedTime;
        }
    }
}