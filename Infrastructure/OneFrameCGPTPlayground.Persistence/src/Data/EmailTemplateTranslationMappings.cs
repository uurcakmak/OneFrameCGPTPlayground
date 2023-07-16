// <copyright file="EmailTemplateTranslationMappings.cs" company="KocSistem">
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
    /// ApplicationRoleMappings.
    /// </summary>
    public static class EmailTemplateTranslationMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<EmailTemplateTranslation> builder)
        {
            _ = builder.ToTable("EmailTemplateTranslation");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="htmlFileName">The builder.</param>
        private static string FillingMailContent(string htmlFileName)
        {
            var isFileExist = File.Exists(Path.Combine("seed-data", "email-templates", htmlFileName));
            return isFileExist ? File.ReadAllText(Path.Combine("seed-data", "email-templates", htmlFileName)) : string.Empty;
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<EmailTemplateTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var dataList = new List<EmailTemplateTranslation>
           {
               new EmailTemplateTranslation
               {
                  Id = Guid.Parse("8D396FB3-675C-477C-8E14-278BB549FBAE"),
                  Language = Common.Enums.LanguageType.en,
                  ReferenceId = Guid.Parse("9459E146-558A-44C5-972E-28A2469B4D1A"),
                  Subject = "You wanted to reset your password",
                  EmailContent = FillingMailContent("forgot-password-en.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = Guid.Parse("B9E86B4A-F377-46CC-8D88-7C2D08E9F3C3"),
                  Language = Common.Enums.LanguageType.tr,
                  ReferenceId = Guid.Parse("9459E146-558A-44C5-972E-28A2469B4D1A"),
                  Subject = "Şifrenizi sıfırlamak istediniz",
                  EmailContent = FillingMailContent("forgot-password-tr.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = Guid.Parse("BE109D28-74D9-4D8A-B155-50F56CCE7758"),
                  Language = Common.Enums.LanguageType.ar,
                  ReferenceId = Guid.Parse("9459E146-558A-44C5-972E-28A2469B4D1A"),
                  Subject = "كنت تريد إعادة تعيين كلمة المرور الخاصة بك",
                  EmailContent = FillingMailContent("forgot-password-ar.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = Guid.Parse("ed93391e-b050-4625-b6e9-4391e349dcd6"),
                  Language = Common.Enums.LanguageType.tr,
                  ReferenceId = Guid.Parse("3d9c744e-5d4a-4f52-822b-e777e301b7cf"),
                  Subject = "Hoşgeldiniz",
                  EmailContent = FillingMailContent("welcome-tr.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = Guid.Parse("c2bf84bb-8f5f-4f11-b6c1-ea1bfcdc915f"),
                  Language = Common.Enums.LanguageType.en,
                  ReferenceId = Guid.Parse("3d9c744e-5d4a-4f52-822b-e777e301b7cf"),
                  Subject = "Welcome",
                  EmailContent = FillingMailContent("welcome-en.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = Guid.Parse("1869f4f7-e8ae-4156-b3ba-4dead5542b66"),
                  Language = Common.Enums.LanguageType.ar,
                  ReferenceId = Guid.Parse("3d9c744e-5d4a-4f52-822b-e777e301b7cf"),
                  Subject = "أهلا بك",
                  EmailContent = FillingMailContent("welcome-ar.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = new Guid("d26fe09f-c2fd-4274-a915-06818937f463"),
                  Language = Common.Enums.LanguageType.en,
                  ReferenceId = new Guid("b811c8ca-d12f-404b-affd-a5dc6e402945"),
                  Subject = "Your Verification Code",
                  EmailContent = FillingMailContent("verification-code-en.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = new Guid("6f22b299-ae8b-495b-a7a3-1847838e6c65"),
                  Language = Common.Enums.LanguageType.tr,
                  ReferenceId = new Guid("b811c8ca-d12f-404b-affd-a5dc6e402945"),
                  Subject = "Doğrulama Kodunuz",
                  EmailContent = FillingMailContent("verification-code-tr.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = new Guid("dc3a6ca1-aa63-43ad-a7a4-42d09c1267f9"),
                  Language = Common.Enums.LanguageType.ar,
                  ReferenceId = new Guid("b811c8ca-d12f-404b-affd-a5dc6e402945"),
                  Subject = "رمز التحقق",
                  EmailContent = FillingMailContent("verification-code-ar.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = new Guid("0d069e15-2a36-46f0-8c14-21dc5d7e5df4"),
                  Language = Common.Enums.LanguageType.en,
                  ReferenceId = new Guid("db0eada7-a618-435e-834c-919ac95a97ce"),
                  Subject = "Email Activation",
                  EmailContent = FillingMailContent("email-activation-en.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = new Guid("3612a90c-e6fb-4059-a281-ab38e69ecdcb"),
                  Language = Common.Enums.LanguageType.tr,
                  ReferenceId = new Guid("db0eada7-a618-435e-834c-919ac95a97ce"),
                  Subject = "Mail Aktivasyon",
                  EmailContent = FillingMailContent("email-activation-tr.html"),
               },
               new EmailTemplateTranslation
               {
                  Id = new Guid("131eaa1c-b3c3-4d24-a2d7-85a612f75295"),
                  Language = Common.Enums.LanguageType.ar,
                  ReferenceId = new Guid("db0eada7-a618-435e-834c-919ac95a97ce"),
                  Subject = "تفعيل البريد الإلكتروني",
                  EmailContent = FillingMailContent("email-activation-ar.html"),
               },
           };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<EmailTemplateTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.HasOne(m => m.Reference).WithMany(m => m.Translations).HasForeignKey(f => f.ReferenceId);
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<EmailTemplateTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.Property(p => p.ReferenceId).IsRequired();

            _ = builder.Property(p => p.Language).IsRequired();

            _ = builder.Property(p => p.Subject).IsRequired().HasMaxLength(int.MaxValue);

            _ = builder.Property(p => p.EmailContent).IsRequired().HasMaxLength(int.MaxValue);
        }
    }
}
