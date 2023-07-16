// <copyright file="LanguageMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneFrameCGPTPlayground.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// LanguageMappings.
    /// </summary>
    public static class LanguageMappings
    {
        public static void OnModelCreating(EntityTypeBuilder<Language> builder)
        {
            _ = builder.ToTable("Language");
            SetProperties(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<Language> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var dataList = new List<Language>
           {
               new Language
               {
                  Id = new Guid("ddec791a-d71f-4c07-91d7-20e4eea2ad7a"),
                  Name = "Türkçe",
                  Code = "tr-TR",
                  IsDefault = false,
                  IsActive = true,
                  Image = File.Exists(Path.Combine("seed-data", "flags", "turkey.svg")) ? "data:image/svg+xml;base64," + Convert.ToBase64String(File.ReadAllBytes(Path.Combine("seed-data", "flags", "turkey.svg"))) : string.Empty,
                  InsertedDate = new DateTime(2022, 1, 1),
                  UpdatedDate = new DateTime(2022, 1, 1),
               },
               new Language
               {
                  Id = new Guid("5a41be5e-0cb9-4a3e-a1a7-0244b53134cc"),
                  Name = "English",
                  Code = "en-EN",
                  IsDefault = true,
                  IsActive = true,
                  Image = File.Exists(Path.Combine("seed-data", "flags", "united-kingdom.svg")) ? "data:image/svg+xml;base64," + Convert.ToBase64String(File.ReadAllBytes(Path.Combine("seed-data", "flags", "united-kingdom.svg"))) : string.Empty,
                  InsertedDate = new DateTime(2022, 1, 1),
                  UpdatedDate = new DateTime(2022, 1, 1),
               },
               new Language
               {
                  Id = new Guid("a5c5d7bd-b5bf-4a71-bc1c-3738ea14297f"),
                  Name = "عربي",
                  Code = "ar-AE",
                  IsDefault = false,
                  IsActive = true,
                  Image = File.Exists(Path.Combine("seed-data", "flags", "saudi-arabia.svg")) ? "data:image/svg+xml;base64," + Convert.ToBase64String(File.ReadAllBytes(Path.Combine("seed-data", "flags", "saudi-arabia.svg"))) : string.Empty,
                  InsertedDate = new DateTime(2022, 1, 1),
                  UpdatedDate = new DateTime(2022, 1, 1),
               },
           };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<Language> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.Property(p => p.Name).IsRequired();

            _ = builder.Property(p => p.Code).IsRequired();

            _ = builder.Property(p => p.Image).IsRequired();

            _ = builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
