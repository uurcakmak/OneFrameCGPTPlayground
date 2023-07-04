// <copyright file="CookieHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Helpers
{
    public static class CookieHelper
    {
        public const string ApplicationCookiePolicy = "ApplicationCookiePolicy";
        public const string Theme = "theme";
        public const string TimeZone = "TimeZone";
        public const string FullName = "FullName";

        public static void Write(HttpResponse response, string cookieName, string cookieValue, string expireDays)
        {
            if (!int.TryParse(expireDays, out var cookieExpireDays))
            {
                cookieExpireDays = 1;
            }

            response.Cookies.Append(cookieName, cookieValue, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(cookieExpireDays), HttpOnly = true });
        }
    }
}
