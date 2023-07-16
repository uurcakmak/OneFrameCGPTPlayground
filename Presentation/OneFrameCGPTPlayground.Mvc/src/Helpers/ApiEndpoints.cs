// <copyright file="ApiEndpoints.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Helpers
{
    public static class ApiEndpoints
    {
        public const string AccountBaseRoute = "accounts/";
        public const string AccountBaseUserRole = AccountBaseRoute + "user-role";
        public const string AccountChangePassword = AccountBaseRoute + "change-password";
        public const string AccountChangePasswordExpired = AccountBaseRoute + "change-password-expired";
        public const string AccountChangeProfileForUser = AccountBaseRoute + "change-profile-info";
        public const string AccountForgotPassword = AccountBaseRoute + "forgot-password";
        public const string AccountGetRoleAssignments = AccountBaseRoute + "role-assignments/{0}";
        public const string AccountGetUserClaimsTree = AccountBaseRoute + "user-claims-tree/{0}";
        public const string AccountIsCaptchaPassed = AccountBaseRoute + "is-captcha-passed/{0}";
        public const string AccountLogin = AccountBaseRoute + "login";
        public const string AccountPagedList = AccountBaseRoute + "list";
        public const string AccountRefresh = AccountBaseRoute + "refresh";
        public const string AccountRegister = AccountBaseRoute + "register";
        public const string AccountResetPassword = AccountBaseRoute + "reset-password";
        public const string AccountSaveUserClaims = AccountBaseRoute + "user-claims";
        public const string AccountSetProfilePhoto = AccountBaseRoute + "set-profile-photo";
        public const string AccountDeleteProfilePhoto = AccountBaseRoute + "delete-profile-photo";
        public const string AccountUserSearch = AccountBaseRoute + "search";
        public const string AccountConfirmEmail = AccountBaseRoute + "confirm-email";
        public const string AccountExternalLogin = AccountBaseRoute + "external-login/{0}";

        public const string ApplicationSettingBaseRoute = "application-settings/";
        public const string ApplicationSettingCategoryBaseRoute = "application-setting-categories/";
        public const string ApplicationSettingCategoryGetById = ApplicationSettingCategoryBaseRoute + "{0}";
        public const string ApplicationSettingCategoryPagedList = ApplicationSettingCategoryBaseRoute + "list";

        public const string ApplicationSettingCategorySearch = ApplicationSettingCategoryBaseRoute + "search";
        public const string ApplicationSettingGetById = ApplicationSettingBaseRoute + "id/{0}";
        public const string ApplicationSettingGetByKey = ApplicationSettingBaseRoute + "{0}";
        public const string ApplicationSettingPagedList = ApplicationSettingBaseRoute + "list";
        public const string ApplicationSettingSearch = ApplicationSettingBaseRoute + "search";

        public const string AuthenticationBaseRoute = "authentications/";
        public const string AuthenticationGenerateAuthenticatorSharedKey = AuthenticationBaseRoute + "generate-authenticator-shared-key";
        public const string AuthenticationSendVerificationCode = AuthenticationBaseRoute + "send-verification-code";
        public const string AuthenticationTwoFactorVerification = AuthenticationBaseRoute + "two-factor-verification";

        public const string ConfigurationBaseRoute = "configurations/";
        public const string ConfigurationFileUploader = ConfigurationBaseRoute + "file-uploader";
        public const string ConfigurationMvcUi = ConfigurationBaseRoute + "mvc-ui";
        public const string ConfigurationTimeZone = ConfigurationBaseRoute + "time-zones";

        public const string LoginAuditLogBaseRoute = "login-audit-logs/";
        public const string LoginAuditLogPagedList = LoginAuditLogBaseRoute + "list";
        public const string LoginAuditLogExcelExport = LoginAuditLogBaseRoute + "export-excel";
        public const string LoginAuditLogPdfExport = LoginAuditLogBaseRoute + "pdf-export";
        public const string LoginAuditLogSearch = LoginAuditLogBaseRoute + "search";

        public const string RoleBaseRoute = "roles/";
        public const string RolePagedList = RoleBaseRoute + "list";
        public const string RoleGetRoleClaimsTree = RoleBaseRoute + "role-claims-tree/{0}";
        public const string RoleGetRoleUserInfo = RoleBaseRoute + "role-user-info/{0}";
        public const string RoleSaveRoleClaims = RoleBaseRoute + "role-claims";
        public const string RoleSearch = RoleBaseRoute + "search";

        public const string User = "users/";
        public const string UserGetUserInfo = User + "user-info";
        public const string UserSetStatus = User + "set-status";
        public const string UserConfirmationCode = User + "user-confirmation-code";
        public const string UserSendConfirmationCode = User + "user-send-code";

        public const string EmailTemplatePagedList = EmailTemplateBaseRoute + "list";
        public const string EmailTemplateBaseRoute = "email-templates/";
        public const string EmailTemplateById = EmailTemplateBaseRoute + "{0}";
        public const string EmailTemplateSearch = EmailTemplateBaseRoute + "search";
        public const string EmailTemplateSendTryMail = EmailTemplateBaseRoute + "send-email";

        public const string EmailNotificationBaseRoute = "email-notifications/";
        public const string EmailNotificationPagedList = EmailNotificationBaseRoute + "list";
        public const string EmailNotificationSearch = EmailNotificationBaseRoute + "search";
        public const string EmailNotificationSend = EmailNotificationBaseRoute + "send/{0}";

        public const string MenuBaseRoute = "menu/";
        public const string MenuTree = MenuBaseRoute + "tree";
        public const string MenuSaveOrder = MenuBaseRoute + "order";

        public const string LanguageBaseRoute = "languages/";
        public const string LanguagePagedList = LanguageBaseRoute + "list";
        public const string Search = LanguageBaseRoute + "search";
        public const string LanguageGetById = LanguageBaseRoute + "{0}";

        public const string ChatCpt = "chatGpt/";
        public const string ChatGptPostUpload = ChatCpt + "upload";
    }
}