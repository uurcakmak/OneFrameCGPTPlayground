// <copyright file="IdentityRoleClaimMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// IdentityRoleClaimMappings.
    /// </summary>
    public static class IdentityRoleClaimMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<IdentityRoleClaim> builder)
        {
            _ = builder.ToTable("RoleClaim");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<IdentityRoleClaim> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var adminId = Guid.Parse("00BB2E85-4474-414C-BED4-6D4FEF568EC4");
            var powerUserId = Guid.Parse("7255E4E1-BCBF-4C1B-89D4-15F3343DC572");
            var guestUserId = Guid.Parse("09c0b51b-f9ac-48a0-8a7c-b5b6b987a4c6");
            var dataList = new List<IdentityRoleClaim<Guid>>
            {
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 1,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleAddClaim,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 2,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleAddUser,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 3,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleClaimList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 4,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleCreate,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 5,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleDelete,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 6,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleEdit,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 7,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 8,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleRemoveClaim,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 9,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleRemoveUser,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 10,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleUserList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 11,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserAddClaim,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 12,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserClaimList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 13,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserCreate,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 14,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserDelete,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 15,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserEdit,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 16,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 17,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserRemoveClaim,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 18,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleAddClaim,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 19,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleAddUser,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 20,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleClaimList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 21,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleDelete,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 22,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleCreate,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 23,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleEdit,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 24,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 25,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleRemoveClaim,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 26,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleRemoveUser,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 27,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleUserList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 28,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 29,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingEdit,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 30,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingDelete,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 31,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingCreate,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 32,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingCategoryList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 33,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingCategoryEdit,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 34,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingCategoryDelete,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 35,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingCategoryCreate,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 36,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ReportLoginAuditLogList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 37,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementExcelExport,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 38,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementEmailTemplateList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 39,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementEmailTemplateEdit,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 40,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ReportEmailNotificationList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 41,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ReportEmailNotificationSend,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 42,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserRole,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 43,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementMenuList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 44,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementMenuEdit,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 45,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementLanguageList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 46,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementLanguageEdit,
                },

                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 47,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingCategoryList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 48,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 49,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementEmailTemplateList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 50,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementExcelExport,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 51,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementLanguageList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 52,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementMenuList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 53,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleClaimList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 54,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 55,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleUserList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 56,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserClaimList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 57,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 58,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ReportEmailNotificationList,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 59,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ReportLoginAuditLogList
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = guestUserId,
                    Id = 60,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementPdfExport,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = adminId,
                    Id = 61,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementPdfExport,
                },
                new IdentityRoleClaim<Guid>
                {
                    RoleId = powerUserId,
                    Id = 62,
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementPdfExport,
                },

                
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<IdentityRoleClaim> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<IdentityRoleClaim> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }
    }
}