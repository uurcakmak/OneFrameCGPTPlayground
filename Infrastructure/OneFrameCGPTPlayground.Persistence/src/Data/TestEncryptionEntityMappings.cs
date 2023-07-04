// <copyright file="TestEncryptionEntityMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    /// <summary>
    /// TestEncryptionEntityMappings.
    /// </summary>
    public static class TestEncryptionEntityMappings
    {
        /// <summary>
        /// Called when /[model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<TestEncryptionEntity> builder)
        {
            _ = builder.ToTable("TestEncryptionEntity");
        }
    }
}
