// <copyright file="ApplicationUserMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneFrameCGPTPlayground.Domain;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// ApplicationUserMappings.
    /// </summary>
    public static class ApplicationUserMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<ApplicationUser> builder)
        {
            _ = builder.ToTable("User");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<ApplicationUser> builder)
        {
            const string adminUserEmail = "ugur.cakmak@kocsistem.com.tr";
            const string securePass = "OneFrame123456";
            var ph = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                AccessFailedCount = 0,
                Id = Guid.Parse("E0CB33F3-591A-4A25-AABA-BD05F796B5FB"),
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = adminUserEmail.ToUpperInvariant(),
                NormalizedUserName = adminUserEmail.ToUpperInvariant(),
                LastPasswordChangedDate = DateTime.Now,
                PhoneNumber = "02165561100",
                PhoneNumberConfirmed = false,
                SecurityStamp = "ce8b16e3-eb01-4263-9418-43103d1a3557",
                TwoFactorEnabled = false,
                UserName = adminUserEmail,
                Name = "Uğur",
                Surname = "Çakmak",
                ConcurrencyStamp = "ab72d39f-7f3a-4bbe-9228-fa2555d8063c",
                Email = adminUserEmail,
                EmailConfirmed = true,
                IsActive = true,
                IsDeleted = false,
                InsertedDate = DateTime.Now,
                InsertedUser = "System",
                TimeZone = "Europe/Istanbul"
            };
            adminUser.PasswordHash = ph.HashPassword(adminUser, securePass);

            var dataList = new List<ApplicationUser>
            {
                adminUser
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<ApplicationUser> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<ApplicationUser> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}