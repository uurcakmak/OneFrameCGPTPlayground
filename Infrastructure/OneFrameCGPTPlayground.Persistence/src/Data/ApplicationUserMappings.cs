// <copyright file="ApplicationUserMappings.cs" company="KocSistem">
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
            const string securePassAdmin = "AQAAAAIAAYagAAAAEJiGxx/WCkz4SZTJQPq3BE3I+38o6kZ2YH5v+qy8dA8+fhSdmB9Gidrz58Gb8zcDSw==";
            const string securePassGuest = "AFob1POC2Udpvc5fqRLaBDsqrmV1DHHVb4XDaflNl9oVSSNha5Dk5dZZPa2f+ynZbQ==";
            const string adminUserEmail = "adminuser@kocsistem.com.tr";
            const string guestUserEmail = "guestuser@kocsistem.com.tr";
            const string oneFrameUserEmail = "oneframeuser@kocsistem.com.tr";
            var dataList = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Id = Guid.Parse("E0CB33F3-591A-4A25-AABA-BD05F796B5FB"),
                    LockoutEnabled = true,
                    LockoutEnd = null,
                    NormalizedEmail = adminUserEmail.ToUpperInvariant(),
                    NormalizedUserName = adminUserEmail.ToUpperInvariant(),
                    PasswordHash = securePassAdmin,
                    LastPasswordChangedDate = new DateTime(2022, 12, 31),
                    PhoneNumber = "02165561100",
                    PhoneNumberConfirmed = false,
                    SecurityStamp = "ce8b16e3-eb01-4263-9418-43103d1a3557",
                    TwoFactorEnabled = false,
                    UserName = adminUserEmail,
                    Name = "Scot",
                    Surname = "Lawson",
                    ConcurrencyStamp = "ab72d39f-7f3a-4bbe-9228-fa2555d8063c",
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    InsertedDate = new DateTime(2021, 12, 31),
                    InsertedUser = "System",
                    TimeZone = "Europe/Istanbul"
                },
                new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Id = Guid.Parse("1C02BE39-802F-4E52-AB19-53FA3E611968"),
                    LockoutEnabled = true,
                    LockoutEnd = null,
                    NormalizedEmail = guestUserEmail.ToUpperInvariant(),
                    NormalizedUserName = guestUserEmail.ToUpperInvariant(),
                    PasswordHash = securePassGuest,
                    LastPasswordChangedDate = new DateTime(2022, 12, 31),
                    PhoneNumber = "02165561100",
                    PhoneNumberConfirmed = false,
                    SecurityStamp = "4b354405-b679-43ed-94ec-28e21c0a7967",
                    TwoFactorEnabled = false,
                    UserName = guestUserEmail,
                    Name = "Melinda",
                    Surname = "Miller",
                    ConcurrencyStamp = "f7c701cc-6e81-48c5-8e81-9a07f0d8f5ff",
                    Email = guestUserEmail,
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    InsertedDate = new DateTime(2021, 12, 31),
                    InsertedUser = "System",
                    TimeZone = "Europe/Istanbul"
                },
                new ApplicationUser
                {
                    AccessFailedCount = 0,
                    Id = Guid.Parse("5F3BBFC9-881E-4968-8803-A8F1ECECACDA"),
                    LockoutEnabled = true,
                    LockoutEnd = null,
                    NormalizedEmail = oneFrameUserEmail.ToUpperInvariant(),
                    NormalizedUserName = oneFrameUserEmail.ToUpperInvariant(),
                    PasswordHash = securePassAdmin,
                    LastPasswordChangedDate = new DateTime(2022, 12, 31),
                    PhoneNumber = "02165561100",
                    PhoneNumberConfirmed = false,
                    SecurityStamp = "6fa8db9a-20b8-403a-b0b3-86242a5c54b1",
                    TwoFactorEnabled = false,
                    UserName = oneFrameUserEmail,
                    Name = "Otto",
                    Surname = "Rinaldi",
                    ConcurrencyStamp = "745ff9fa-5f14-4be4-90c3-2c2c50ef4458",
                    Email = oneFrameUserEmail,
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    InsertedDate = new DateTime(2021, 12, 31),
                    InsertedUser = "System",
                    TimeZone = "Europe/Istanbul"
                },
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