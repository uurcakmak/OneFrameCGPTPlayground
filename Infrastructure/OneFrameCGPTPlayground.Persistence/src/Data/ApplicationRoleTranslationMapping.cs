// <copyright file="ApplicationRoleTranslationMapping.cs" company="KocSistem">
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
    /// ApplicationRoleMappings.
    /// </summary>
    public static class ApplicationRoleTranslationMapping
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<ApplicationRoleTranslation> builder)
        {
            _ = builder.ToTable("RoleTranslation");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<ApplicationRoleTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var adminRoleId = Guid.Parse("00BB2E85-4474-414C-BED4-6D4FEF568EC4");
            var guestRoleId = Guid.Parse("09C0B51B-F9AC-48A0-8A7C-B5B6B987A4C6");
            var powerUserRoleId = Guid.Parse("7255E4E1-BCBF-4C1B-89D4-15F3343DC572");

            var dataList = new List<ApplicationRoleTranslation>
           {
               new ApplicationRoleTranslation
               {
                  Id = Guid.Parse("C3877E22-8FAD-4CD2-8480-F629F0D481A2"),
                  DisplayText = "Admin",
                  Language = Common.Enums.LanguageType.en,
                  ReferenceId = adminRoleId,
                  Description = $"Admin Description",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("F376D12E-3ECA-4255-8592-41946E808B3C"),
                   DisplayText = "Yönetici",
                   Language = Common.Enums.LanguageType.tr,
                   ReferenceId = adminRoleId,
                   Description = $"Yönetici Açıklaması",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("40434644-6DDE-464D-B65B-1A5E5AC9E5D6"),
                   DisplayText = "مشرف",
                   Language = Common.Enums.LanguageType.ar,
                   ReferenceId = adminRoleId,
                   Description = $"وصف المسؤول",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("03875513-2D3D-48EE-A4D8-927CA716BA00"),
                   DisplayText = "Guest",
                   Language = Common.Enums.LanguageType.en,
                   ReferenceId = guestRoleId,
                   Description = $"Guest Description",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("83AD9A8B-A2EA-4DE0-B9DD-01AF7AAFAAC7"),
                   DisplayText = "Ziyaretçi",
                   Language = Common.Enums.LanguageType.tr,
                   ReferenceId = guestRoleId,
                   Description = $"Ziyaretçi Açıklaması",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("A7465DF1-896B-4C73-BC67-81BD50B191CA"),
                   DisplayText = "زائر",
                   Language = Common.Enums.LanguageType.ar,
                   ReferenceId = guestRoleId,
                   Description = $"وصف الضيف",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("F9CA3EFB-8334-46A6-807F-C2881990C273"),
                   DisplayText = "Power User",
                   Language = Common.Enums.LanguageType.en,
                   ReferenceId = powerUserRoleId,
                   Description = $"Power User Description",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("E64211A1-3E2A-4906-B4F7-F1864B9AC777"),
                   DisplayText = "Güçlü Kullanıcı",
                   Language = Common.Enums.LanguageType.tr,
                   ReferenceId = powerUserRoleId,
                   Description = $"Güçlü Kullanıcı Açıklaması",
               },
               new ApplicationRoleTranslation
               {
                   Id = Guid.Parse("A5111850-8D65-48AD-A3D8-077213FDE69A"),
                   DisplayText = "مستخدم متميز",
                   Language = Common.Enums.LanguageType.ar,
                   ReferenceId = powerUserRoleId,
                   Description = $"وصف المستخدم القوي",
               },
           };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<ApplicationRoleTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            // If you need it. Use it here.
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<ApplicationRoleTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.Property(p => p.ReferenceId).IsRequired();

            _ = builder.Property(p => p.Language).IsRequired();

            _ = builder.Property(p => p.DisplayText).IsRequired().HasMaxLength(int.MaxValue);
        }
    }
}