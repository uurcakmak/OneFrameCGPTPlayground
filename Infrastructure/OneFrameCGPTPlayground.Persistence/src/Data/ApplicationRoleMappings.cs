// <copyright file="ApplicationRoleMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneFrameCGPTPlayground.Common.Helpers;
using OneFrameCGPTPlayground.Domain;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// ApplicationRoleMappings.
    /// </summary>
    public static class ApplicationRoleMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<ApplicationRole> builder)
        {
            _ = builder.ToTable("Role");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<ApplicationRole> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var dataList = new List<ApplicationRole>
           {
               new ApplicationRole
               {
                  Id = Guid.Parse("00BB2E85-4474-414C-BED4-6D4FEF568EC4"),
                  Name = Role.Admin.ToString(),
                  NormalizedName = Role.Admin.ToString().ToUpperInvariant(),
                  ConcurrencyStamp = "4b2b5364-a3a5-45d1-884a-bd89e93a759b",
               },
               new ApplicationRole
               {
                   Id = Guid.Parse("09C0B51B-F9AC-48A0-8A7C-B5B6B987A4C6"),
                   Name = Role.Guest.ToString(),
                   NormalizedName = Role.Guest.ToString().ToUpperInvariant(),
                   ConcurrencyStamp = "2b1129f9-b878-4311-ad1d-833f1ce71ca6",
               },
               new ApplicationRole
               {
                   Id = Guid.Parse("7255E4E1-BCBF-4C1B-89D4-15F3343DC572"),
                   Name = Role.PowerUser.ToString(),
                   NormalizedName = Role.PowerUser.ToString().ToUpperInvariant(),
                   ConcurrencyStamp = "225ab332-e2d0-4ef7-a31f-752528fcaf15",
               },
           };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<ApplicationRole> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<ApplicationRole> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));
            _ = builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        }
    }
}