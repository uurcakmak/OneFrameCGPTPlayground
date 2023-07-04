// <copyright file="IdentityUserRoleMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

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
    /// IdentityUserRoleMappings.
    /// </summary>
    public static class IdentityUserRoleMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<IdentityUserRole> builder)
        {
            _ = builder.ToTable("UserRole");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<IdentityUserRole> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var dataList = new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("00BB2E85-4474-414C-BED4-6D4FEF568EC4"), // Admin
                    UserId = Guid.Parse("E0CB33F3-591A-4A25-AABA-BD05F796B5FB"), // adminuser@kocsistem.com.tr
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("09C0B51B-F9AC-48A0-8A7C-B5B6B987A4C6"), // Guest
                    UserId = Guid.Parse("1C02BE39-802F-4E52-AB19-53FA3E611968"), // guestuser@kocsistem.com.tr
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = Guid.Parse("7255E4E1-BCBF-4C1B-89D4-15F3343DC572"), // PowerUser
                    UserId = Guid.Parse("5F3BBFC9-881E-4968-8803-A8F1ECECACDA"), // oneframeuser@kocsistem.com.tr
                },
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<IdentityUserRole> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<IdentityUserRole> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }
    }
}