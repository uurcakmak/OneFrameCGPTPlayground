// -----------------------------------------------------------------------
// <copyright file="EmailTemplateMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using KocSistem.OneFrame.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneFrameCGPTPlayground.Domain;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// EmailTemplateMappings.
    /// </summary>
    public static class EmailTemplateMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<EmailTemplate> builder)
        {
            _ = builder.ToTable("EmailTemplate");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<EmailTemplate> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var dataList = new List<EmailTemplate>
            {
                new EmailTemplate()
                {
                    Id = Guid.Parse("9459E146-558A-44C5-972E-28A2469B4D1A"),
                    IsDeleted = false,
                    Name = "ForgotPassword",
                },
                new EmailTemplate()
                {
                    Id = Guid.Parse("3d9c744e-5d4a-4f52-822b-e777e301b7cf"),
                    IsDeleted = false,
                    Name = "Welcome",
                },
                new EmailTemplate()
                {
                    Id = Guid.Parse("b811c8ca-d12f-404b-affd-a5dc6e402945"),
                    IsDeleted = false,
                    Name = "TwoFAVerificationCode",
                },
                new EmailTemplate()
                {
                    Id = Guid.Parse("db0eada7-a618-435e-834c-919ac95a97ce"),
                    IsDeleted = false,
                    Name = "EmailActivation",
                }
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<EmailTemplate> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<EmailTemplate> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}