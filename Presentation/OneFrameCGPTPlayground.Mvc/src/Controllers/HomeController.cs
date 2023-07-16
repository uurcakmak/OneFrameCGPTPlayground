// <copyright file="HomeController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models;
using OneFrameCGPTPlayground.Mvc.Models.Home;
using OneFrameCGPTPlayground.Mvc.Models.Other;
using OneFrameCGPTPlayground.Mvc.Models.Profile;
using System.Diagnostics;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IConfiguration _configuration;

        public HomeController(IKsI18N i18N, IConfiguration configuration)
            : base(i18N)
        {
            _configuration = configuration;
        }

        public static string CheckThemeName(string themeName)
        {
            var themeList = new List<string> { Themes.Dark, Themes.Light };

            if (string.IsNullOrEmpty(themeName) || themeList.All(a => a != themeName))
            {
                themeName = themeList.First();
            }

            return themeName;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var viewModel = new IndexModel();

            var profileResponse = await GetApiRequestAsync<ServiceResponse<ProfileModel>>(ApiEndpoints.UserGetUserInfo).ConfigureAwait(false);

            if (!profileResponse.IsSuccessful)
            {
                return ToastError(profileResponse.Error);
            }

            viewModel.Profile = profileResponse.Result;
            return View(viewModel);
        }

        [HttpGet("set-language")]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            CookieHelper.Write(Response, CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), _configuration["CookieSettings:LanguageExpiredDays"]);

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet("set-theme")]
        [AllowAnonymous]
        public IActionResult SetTheme(string themeValue, string returnUrl)
        {
            themeValue = CheckThemeName(themeValue);

            CookieHelper.Write(Response, CookieHelper.Theme, themeValue, _configuration["CookieSettings:ThemeExpiredDays"]);

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet("set-cookie-policy")]
        [AllowAnonymous]
        public IActionResult SetCookiePolicy(ApplicationCookiePolicy policy, string returnUrl)
        {
            CookieHelper.Write(Response, CookieHelper.ApplicationCookiePolicy, JsonConvert.SerializeObject(policy), _configuration["CookieSettings:ApplicationCookiePolicyExpiredDays"]);

            return Json(returnUrl ?? "/");
        }

        public IActionResult Error()
        {
            int statusCode = HttpContext.Response.StatusCode;

            if (statusCode == StatusCodes.Status404NotFound)
            {
                return View(statusCode.ToString());
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}