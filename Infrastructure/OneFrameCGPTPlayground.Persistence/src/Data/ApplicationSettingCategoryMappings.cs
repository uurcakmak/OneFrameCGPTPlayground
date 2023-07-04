// <copyright file="ApplicationSettingCategoryMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// ApplicationSettingCategoryMappings.
    /// </summary>
    public static class ApplicationSettingCategoryMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<ApplicationSettingCategory> builder)
        {
            _ = builder.ToTable("ApplicationSettingCategory");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<ApplicationSettingCategory> builder)
        {
            var dataList = new List<ApplicationSettingCategory>
            {
                new ApplicationSettingCategory
                {
                    Id = new Guid("0637d6bb-ea07-4d50-a9cb-e6be151e743a"),
                    Name = "system-shared",
                    Description = "Startup configurations for all applications",
                },
                new ApplicationSettingCategory
                {
                    Id = new Guid("528f6ed0-dea7-4364-9670-15982c59b7ff"),
                    Name = "system-web-api",
                    Description = "Startup specific configurations for Web API",
                },
                new ApplicationSettingCategory
                {
                    Id = new Guid("d438be21-d852-4417-abf7-f2c188176859"),
                    Name = "system-mvc-ui",
                    Description = "Startup specific configurations for MVC UI",
                },
                new ApplicationSettingCategory
                {
                    Id = new Guid("5482df4f-158e-404f-8b82-f86a5c3f0f19"),
                    Name = "system-react",
                    Description = "Startup specific configurations for React UI",
                },
                new ApplicationSettingCategory
                {
                    Id = new Guid("f08bb24d-6b2c-4c48-bb55-6e7cd1c233c1"),
                    Name = "system-hangfire",
                    Description = "Startup specific configurations for Hangfire",
                },
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<ApplicationSettingCategory> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<ApplicationSettingCategory> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.HasIndex(e => e.Name).IsUnique();
            _ = builder.Property(p => p.Name).IsRequired().HasMaxLength(150);

            _ = builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}