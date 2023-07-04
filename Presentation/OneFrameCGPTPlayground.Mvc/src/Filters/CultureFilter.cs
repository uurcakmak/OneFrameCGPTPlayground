// <copyright file="CultureFilter.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace OneFrameCGPTPlayground.Mvc.Filters
{
    public class CultureFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var locOptions = (IOptions<RequestLocalizationOptions>)context.HttpContext.RequestServices.GetService(typeof(IOptions<RequestLocalizationOptions>));

            var cultureCookie = context.HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

            CultureInfo currentCulture;
            CultureInfo currentUiCulture;
            var cookieOption = new CookieOptions { HttpOnly = true };
            if (string.IsNullOrEmpty(cultureCookie))
            {
                currentCulture = locOptions.Value.DefaultRequestCulture.Culture;
                currentUiCulture = locOptions.Value.DefaultRequestCulture.UICulture;
                var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(currentCulture, currentUiCulture));
                context.HttpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, cookieValue, cookieOption);
            }
            else
            {
                var cookieValueResult = CookieRequestCultureProvider.ParseCookieValue(cultureCookie);
                if (cookieValueResult != null &&
                    locOptions.Value.SupportedCultures.Contains(new CultureInfo(cookieValueResult.Cultures.First().ToString()))
                    && locOptions.Value.SupportedUICultures.Contains(new CultureInfo(cookieValueResult.UICultures.First().ToString())))
                {
                    currentCulture = new CultureInfo(cookieValueResult.Cultures.First().ToString());
                    currentUiCulture = new CultureInfo(cookieValueResult.UICultures.First().ToString());
                }
                else
                {
                    currentCulture = locOptions.Value.DefaultRequestCulture.Culture;
                    currentUiCulture = locOptions.Value.DefaultRequestCulture.UICulture;
                    var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(currentCulture, currentUiCulture));
                    context.HttpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, cookieValue, cookieOption);
                }
            }

            CultureInfo.CurrentCulture = currentCulture;
            CultureInfo.CurrentUICulture = currentUiCulture;

            CultureInfo.DefaultThreadCurrentCulture = currentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = currentUiCulture;
        }
    }
}