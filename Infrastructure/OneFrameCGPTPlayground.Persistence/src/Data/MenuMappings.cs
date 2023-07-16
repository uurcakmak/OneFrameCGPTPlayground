// <copyright file="MenuMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneFrameCGPTPlayground.Common.Authentication;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// MenuMappings.
    /// </summary>
    public static class MenuMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<Domain.Menu> builder)
        {
            _ = builder.ToTable("Menu");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<Domain.Menu> builder)
        {
            var orderGroupList = new Dictionary<string, int>
            {
                { "UserManagement", 996 },
                { "RoleManagement", 997 },
                { "Reports", 998 },
                { "SettingManagement", 999 },
            };

            var dataList = new List<Domain.Menu>
            {
                new Domain.Menu()
                {
                    Id = 1,
                    Icon = "user-circle",
                    ParentId = null,
                    Name = "User Management",
                    Url = null,
                    ClaimType = null,
                    ClaimValue = null,
                    OrderId = orderGroupList["UserManagement"],
                },
                new Domain.Menu()
                {
                    Id = 2,
                    Icon = "user",
                    ParentId = 1,
                    Name = "Users",
                    Url = "/users",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserList,
                    OrderId = 1,
                },
                new Domain.Menu()
                {
                    Id = 3,
                    Icon = "id-badge",
                    ParentId = 1,
                    Name = "User Claim Management",
                    Url = "/users/user-claims",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementUserClaimList,
                    OrderId = 2,
                },
                new Domain.Menu()
                {
                    Id = 4,
                    Icon = "id-card",
                    ParentId = null,
                    Name = "Role Management",
                    Url = null,
                    ClaimType = null,
                    ClaimValue = null,
                    OrderId = orderGroupList["RoleManagement"],
                },
                new Domain.Menu()
                {
                    Id = 5,
                    Icon = "flag",
                    ParentId = 4,
                    Name = "Roles",
                    Url = "/roles",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleList,
                    OrderId = 1,
                },
                new Domain.Menu()
                {
                    Id = 6,
                    Icon = "id-badge",
                    ParentId = 4,
                    Name = "Role Claim Management",
                    Url = "/roles/role-claims",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementRoleClaimList,
                    OrderId = 2,
                },
                new Domain.Menu()
                {
                    Id = 7,
                    Icon = "building",
                    ParentId = null,
                    Name = "Setting Management",
                    Url = null,
                    ClaimType = null,
                    ClaimValue = null,
                    OrderId = orderGroupList["SettingManagement"],
                },
                new Domain.Menu()
                {
                    Id = 8,
                    Icon = "square",
                    ParentId = 7,
                    Name = "Application Settings",
                    Url = "/application-settings",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingList,
                    OrderId = 2,
                },
                new Domain.Menu()
                {
                    Id = 9,
                    Icon = "square",
                    ParentId = 7,
                    Name = "Application Setting Categories",
                    Url = "/application-setting-categories",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementApplicationSettingCategoryList,
                    OrderId = 1,
                },
                new Domain.Menu()
                {
                    Id = 10,
                    Icon = "file",
                    ParentId = null,
                    Name = "Reports",
                    Url = null,
                    ClaimType = null,
                    ClaimValue = null,
                    OrderId = orderGroupList["Reports"],
                },
                new Domain.Menu()
                {
                    Id = 11,
                    Icon = "file-alt",
                    ParentId = 10,
                    Name = "Login Audit Logs",
                    Url = "/login-audit-logs",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ReportLoginAuditLogList,
                    OrderId = 1,
                },
                new Domain.Menu()
                {
                    Id = 12,
                    Icon = "envelope",
                    ParentId = 7,
                    Name = "Email Template",
                    Url = "/email-templates",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementEmailTemplateList,
                },
                new Domain.Menu()
                {
                    Id = 13,
                    Icon = "envelope",
                    ParentId = 10,
                    Name = "Email Notifications",
                    Url = "/email-notifications",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ReportEmailNotificationList,
                    OrderId = 2,
                },
                new Domain.Menu()
                {
                    Id = 14,
                    Icon = "list-alt",
                    ParentId = 7,
                    Name = "Menu Order",
                    Url = "/menus/order",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementMenuList,
                    OrderId = 3,
                },
                new Domain.Menu()
                {
                    Id = 15,
                    Icon = "flag",
                    ParentId = 7,
                    Name = "Languages",
                    Url = "/languages",
                    ClaimType = ApplicationPolicyType.KsPermission,
                    ClaimValue = KsPermissionPolicy.ManagementLanguageList,
                }
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<Domain.Menu> builder)
        {
            _ = builder.HasOne(m => m.ParentMenu).WithMany(m => m.ChildMenu).HasForeignKey(f => f.ParentId);
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<Domain.Menu> builder)
        {
            _ = builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            _ = builder.Property(p => p.OrderId).HasDefaultValue(999);
        }
    }
}