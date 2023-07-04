// <copyright file="ApplicationSettingMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// ApplicationSettingMappings.
    /// </summary>
    public static class ApplicationSettingMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<ApplicationSetting> builder)
        {
            _ = builder.ToTable("ApplicationSetting");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<ApplicationSetting> builder)
        {
            var shared = new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a");

            var dataList = new List<ApplicationSetting>
            {
                new ApplicationSetting
                {
                    Id = new Guid("3da733ae-b51c-45cb-893d-355b0a0ee0a7"),
                    Key = ConfigurationConstant.Identity2FaSettingsAuthenticatorLinkName,
                    Value = "OneFrame",
                    ValueType = typeof(string).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("84f55a47-6c93-44c1-803b-35f2170b4f3a"),
                    Key = ConfigurationConstant.Identity2FaSettingsIsEnabled,
                    Value = "false",
                    ValueType = typeof(bool).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("e516a25c-701e-454f-b569-dc6893725480"),
                    Key = ConfigurationConstant.Identity2FaSettingsType,
                    Value = "Authenticator",
                    ValueType = typeof(string).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("a670744c-02a6-4cb8-bb39-7a799479e3a0"),
                    Key = ConfigurationConstant.Identity2FaSettingsVerificationTime,
                    Value = "60",
                    ValueType = typeof(int).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("04d858bf-6ec6-4753-b0a3-60e0a81dfd09"),
                    Key = ConfigurationConstant.IdentityAutoLogoutDialogTimeout,
                    Value = "30000", // 30 seconds
                    ValueType = typeof(int).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("09783eac-26f9-4bec-8ef4-8c9e47ed58e2"),
                    Key = ConfigurationConstant.IdentityAutoLogoutIdleTimeout,
                    Value = "600000", // 10 minutes
                    ValueType = typeof(int).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("127d4b3e-2f09-40a7-9033-448cb9dfd187"),
                    Key = ConfigurationConstant.IdentityProfilePhotoMaxSize,
                    Value = "160000",
                    ValueType = typeof(int).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("918f1747-c7c4-49c1-98a4-042b4618c8a7"),
                    Key = ConfigurationConstant.IdentityAutoLogoutIsEnabled,
                    Value = "true",
                    ValueType = typeof(bool).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                },
                new ApplicationSetting
                {
                    Id = new Guid("11cab984-9b33-49e5-b803-f4e49986d55d"),
                    Key = ConfigurationConstant.NotificationEmailIsActive,
                    Value = "true",
                    ValueType = typeof(bool).FullName,
                    IsStatic = true,
                    CategoryId = shared,
                }
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<ApplicationSetting> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.HasOne(m => m.Category).WithMany(m => m.ApplicationSettings).HasForeignKey(f => f.CategoryId);
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<ApplicationSetting> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.HasIndex(e => e.Key).IsUnique();
            _ = builder.Property(p => p.Key).IsRequired().HasMaxLength(150);

            _ = builder.Property(p => p.ValueType).IsRequired().HasMaxLength(50);

            _ = builder.Property(p => p.Value).IsRequired().HasMaxLength(500);

            _ = builder.Property(p => p.CategoryId).IsRequired();
        }
    }
}