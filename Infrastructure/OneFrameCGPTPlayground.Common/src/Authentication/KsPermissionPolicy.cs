// <copyright file="KsPermissionPolicy.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Common.Authentication
{
    /// <summary>
    /// KsPermissionPolicy.
    /// </summary>
    public static class KsPermissionPolicy
    {
        public const string ManagementRoleAddClaim = "Management_Role_AddClaim";

        public const string ManagementRoleAddUser = "Management_Role_AddUser";

        public const string ManagementRoleClaimList = "Management_Role_ClaimList";

        public const string ManagementRoleCreate = "Management_Role_Create";

        public const string ManagementRoleDelete = "Management_Role_Delete";

        public const string ManagementRoleEdit = "Management_Role_Edit";

        public const string ManagementRoleList = "Management_Role_List";

        public const string ManagementRoleRemoveClaim = "Management_Role_RemoveClaim";

        public const string ManagementRoleRemoveUser = "Management_Role_RemoveUser";

        public const string ManagementRoleUserList = "Management_Role_UserList";

        public const string ManagementUserAddClaim = "Management_User_AddClaim";

        public const string ManagementUserClaimList = "Management_User_ClaimList";

        public const string ManagementUserCreate = "Management_User_Create";

        public const string ManagementUserDelete = "Management_User_Delete";

        public const string ManagementUserEdit = "Management_User_Edit";

        public const string ManagementUserRole = "Management_User_Role";

        public const string ManagementUserList = "Management_User_List";

        public const string ManagementUserRemoveClaim = "Management_User_RemoveClaim";

        public const string ManagementApplicationSettingList = "Management_ApplicationSetting_List";

        public const string ManagementApplicationSettingCreate = "Management_ApplicationSetting_Create";

        public const string ManagementApplicationSettingEdit = "Management_ApplicationSetting_Edit";

        public const string ManagementApplicationSettingDelete = "Management_ApplicationSetting_Delete";

        public const string ManagementApplicationSettingCategoryList = "Management_ApplicationSettingCategory_List";

        public const string ManagementApplicationSettingCategoryCreate = "Management_ApplicationSettingCategory_Create";

        public const string ManagementApplicationSettingCategoryEdit = "Management_ApplicationSettingCategory_Edit";

        public const string ManagementApplicationSettingCategoryDelete = "Management_ApplicationSettingCategory_Delete";

        public const string ManagementEmailTemplateList = "Management_EmailTemplate_List";

        public const string ManagementEmailTemplateEdit = "Management_EmailTemplate_Edit";

        public const string ReportLoginAuditLogList = "Report_LoginAuditLog_List";

        public const string ManagementExcelExport = "Management_Excel_Export";

        public const string ManagementPdfExport = "Management_Pdf_Export";

        public const string ManagementLanguageList = "Management_Language_List";

        public const string ManagementLanguageEdit = "Management_Language_Edit";

        public const string ManagementMenuList = "Management_Menu_List";

        public const string ManagementMenuEdit = "Management_Menu_Edit";

        [Obsolete("not used 123")]
        [Display(GroupName = "Old", Name = "Not used", Description = "example of old permission")]
        public const string ObsoleteSample = "ObsoleteSample";

        public const string ReportEmailNotificationList = "Report_EmailNotification_List";

        public const string ReportEmailNotificationSend = "Report_EmailNotification_Send";
    }
}