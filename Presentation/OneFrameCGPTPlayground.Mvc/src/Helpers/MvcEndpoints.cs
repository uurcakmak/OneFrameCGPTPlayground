// <copyright file="MvcEndpoints.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Helpers
{
    public static class MvcEndpoints
    {
        public const string AccountBaseRoute = "/accounts";
        public const string AccountPasswordExpired = AccountBaseRoute + "/password-expired";
        public const string ApplicationSettingBaseRoute = "/application-settings";
        public const string ApplicationSettingCategoryBaseRoute = "/application-setting-categories";
        public const string EmailTemplateBaseRoute = "/email-templates";
        public const string ProfileBaseRoute = "/profiles";
        public const string RoleBaseRoute = "/roles";
        public const string UserBaseRoute = "/users";
        public const string EmailNotificationBaseRoute = "/email-notifications";
        public const string MenuBaseRoute = "/menus";
        public const string MenuOrder = MenuBaseRoute + "/order";
        public const string LanguageBaseRoute = "/languages";

        public const string ProfileConfirmPhoneNumberRoute = ProfileBaseRoute + "/confirm-phonenumber";
    }
}