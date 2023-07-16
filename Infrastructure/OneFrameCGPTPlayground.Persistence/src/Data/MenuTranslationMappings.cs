// <copyright file="MenuTranslationMappings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OneFrameCGPTPlayground.Common.Enums;
using OneFrameCGPTPlayground.Domain;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Persistence.Data
{
    public static class MenuTranslationMappings
    {
        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(EntityTypeBuilder<MenuTranslation> builder)
        {
            _ = builder.ToTable("MenuTranslation");

            SetProperties(builder);
            SetForeignKeys(builder);
            SeedData(builder);
        }

        /// <summary>
        /// Seeds the data.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SeedData(EntityTypeBuilder<MenuTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            var dataList = new List<MenuTranslation>
            {
                new MenuTranslation
                {
                    Id = 1,
                    Language = LanguageType.en,
                    ReferenceId = 1,
                    DisplayText = "User Management",
                },
                new MenuTranslation
                {
                    Id = 2,
                    Language = LanguageType.tr,
                    ReferenceId = 1,
                    DisplayText = "Kullanıcı Yönetimi",
                },
                new MenuTranslation
                {
                    Id = 3,
                    Language = LanguageType.ar,
                    ReferenceId = 1,
                    DisplayText = "إدارةالمستخدم",
                },
                new MenuTranslation
                {
                    Id = 4,
                    Language = LanguageType.en,
                    ReferenceId = 2,
                    DisplayText = "Users",
                },
                new MenuTranslation
                {
                    Id = 5,
                    Language = LanguageType.tr,
                    ReferenceId = 2,
                    DisplayText = "Kullanıcılar",
                },
                new MenuTranslation
                {
                    Id = 6,
                    Language = LanguageType.ar,
                    ReferenceId = 2,
                    DisplayText = "المستخدمون",
                },
                new MenuTranslation
                {
                    Id = 7,
                    Language = LanguageType.en,
                    ReferenceId = 3,
                    DisplayText = "User Claim Management",
                },
                new MenuTranslation
                {
                    Id = 8,
                    Language = LanguageType.tr,
                    ReferenceId = 3,
                    DisplayText = "Kullanıcı Yetki Yönetimi",
                },
                new MenuTranslation
                {
                    Id = 9,
                    Language = LanguageType.ar,
                    ReferenceId = 3,
                    DisplayText = "إدارة مطالبة المستخدم",
                },
                new MenuTranslation
                {
                    Id = 10,
                    Language = LanguageType.en,
                    ReferenceId = 4,
                    DisplayText = "Role Management",
                },
                new MenuTranslation
                {
                    Id = 11,
                    Language = LanguageType.tr,
                    ReferenceId = 4,
                    DisplayText = "Rol Yönetimi",
                },
                new MenuTranslation
                {
                    Id = 12,
                    Language = LanguageType.ar,
                    ReferenceId = 4,
                    DisplayText = "إدارة الأدوار",
                },
                new MenuTranslation
                {
                    Id = 13,
                    Language = LanguageType.en,
                    ReferenceId = 5,
                    DisplayText = "Roles",
                },
                new MenuTranslation
                {
                    Id = 14,
                    Language = LanguageType.tr,
                    ReferenceId = 5,
                    DisplayText = "Roller",
                },
                new MenuTranslation
                {
                    Id = 15,
                    Language = LanguageType.ar,
                    ReferenceId = 5,
                    DisplayText = "الأدوار",
                },
                new MenuTranslation
                {
                    Id = 16,
                    Language = LanguageType.en,
                    ReferenceId = 6,
                    DisplayText = "Role Claim Management",
                },
                new MenuTranslation
                {
                    Id = 17,
                    Language = LanguageType.tr,
                    ReferenceId = 6,
                    DisplayText = "Rol Yetki Yönetimi",
                },
                new MenuTranslation
                {
                    Id = 18,
                    Language = LanguageType.ar,
                    ReferenceId = 6,
                    DisplayText = "إدارة مطالبات الدور",
                },
                new MenuTranslation
                {
                    Id = 19,
                    Language = LanguageType.en,
                    ReferenceId = 7,
                    DisplayText = "Setting Management",
                },
                new MenuTranslation
                {
                    Id = 20,
                    Language = LanguageType.tr,
                    ReferenceId = 7,
                    DisplayText = "Ayar Yönetimi",
                },
                new MenuTranslation
                {
                    Id = 21,
                    Language = LanguageType.ar,
                    ReferenceId = 7,
                    DisplayText = "وضع الإدارة",
                },
                new MenuTranslation
                {
                    Id = 22,
                    Language = LanguageType.en,
                    ReferenceId = 8,
                    DisplayText = "Application Settings",
                },
                new MenuTranslation
                {
                    Id = 23,
                    Language = LanguageType.tr,
                    ReferenceId = 8,
                    DisplayText = "Uygulama Ayarları",
                },
                new MenuTranslation
                {
                    Id = 24,
                    Language = LanguageType.ar,
                    ReferenceId = 8,
                    DisplayText = "إعدادات التطبيق",
                },
                new MenuTranslation
                {
                    Id = 25,
                    Language = LanguageType.en,
                    ReferenceId = 9,
                    DisplayText = "Application Setting Categories",
                },
                new MenuTranslation
                {
                    Id = 26,
                    Language = LanguageType.tr,
                    ReferenceId = 9,
                    DisplayText = "Uygulama Ayar Kategorileri",
                },
                new MenuTranslation
                {
                    Id = 27,
                    Language = LanguageType.ar,
                    ReferenceId = 9,
                    DisplayText = "فئات إعداد التطبيق",
                },
                new MenuTranslation
                {
                    Id = 28,
                    Language = LanguageType.en,
                    ReferenceId = 10,
                    DisplayText = "Reports",
                },
                new MenuTranslation
                {
                    Id = 29,
                    Language = LanguageType.tr,
                    ReferenceId = 10,
                    DisplayText = "Raporlar",
                },
                new MenuTranslation
                {
                    Id = 30,
                    Language = LanguageType.ar,
                    ReferenceId = 10,
                    DisplayText = "نقل",
                },
                new MenuTranslation
                {
                    Id = 31,
                    Language = LanguageType.en,
                    ReferenceId = 11,
                    DisplayText = "Login Audit Log",
                },
                new MenuTranslation
                {
                    Id = 32,
                    Language = LanguageType.tr,
                    ReferenceId = 11,
                    DisplayText = "Giriş Denetim Günlüğü",
                },
                new MenuTranslation
                {
                    Id = 33,
                    Language = LanguageType.ar,
                    ReferenceId = 11,
                    DisplayText = "سجل تدقيق تسجيل الدخول",
                },
                new MenuTranslation
                {
                    Id = 34,
                    Language = LanguageType.en,
                    ReferenceId = 12,
                    DisplayText = "Email Templates",
                },
                new MenuTranslation
                {
                    Id = 35,
                    Language = LanguageType.tr,
                    ReferenceId = 12,
                    DisplayText = "E-posta Şablonları",
                },
                new MenuTranslation
                {
                    Id = 36,
                    Language = LanguageType.ar,
                    ReferenceId = 12,
                    DisplayText = "قوالب البريد الإلكتروني",
                },
                new MenuTranslation
                {
                    Id = 37,
                    Language = LanguageType.en,
                    ReferenceId = 13,
                    DisplayText = "Email Notifications",
                },
                new MenuTranslation
                {
                    Id = 38,
                    Language = LanguageType.tr,
                    ReferenceId = 13,
                    DisplayText = "E-posta Bildirimleri",
                },
                new MenuTranslation
                {
                    Id = 39,
                    Language = LanguageType.ar,
                    ReferenceId = 13,
                    DisplayText = "اشعارات البريد الالكتروني",
                },
                new MenuTranslation
                {
                    Id = 40,
                    Language = LanguageType.en,
                    ReferenceId = 14,
                    DisplayText = "Menu Order Management",
                },
                new MenuTranslation
                {
                    Id = 41,
                    Language = LanguageType.tr,
                    ReferenceId = 14,
                    DisplayText = "Menü Sıralama Yönetimi",
                },
                new MenuTranslation
                {
                    Id = 42,
                    Language = LanguageType.ar,
                    ReferenceId = 14,
                    DisplayText = "إدارة فرز القائمة",
                },
                new MenuTranslation
                {
                    Id = 43,
                    Language = LanguageType.en,
                    ReferenceId = 15,
                    DisplayText = "Languages",
                },
                new MenuTranslation
                {
                    Id = 44,
                    Language = LanguageType.tr,
                    ReferenceId = 15,
                    DisplayText = "Diller",
                },
                new MenuTranslation
                {
                    Id = 45,
                    Language = LanguageType.ar,
                    ReferenceId = 15,
                    DisplayText = "اللغة",
                }
            };

            _ = builder.HasData(dataList);
        }

        /// <summary>
        /// Sets the foreign keys.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetForeignKeys(EntityTypeBuilder<MenuTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.HasOne(m => m.Reference).WithMany(m => m.Translations).HasForeignKey(f => f.ReferenceId);
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private static void SetProperties(EntityTypeBuilder<MenuTranslation> builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            _ = builder.Property(p => p.ReferenceId).IsRequired();

            _ = builder.Property(p => p.Language).IsRequired();

            _ = builder.Property(p => p.DisplayText).IsRequired().HasMaxLength(int.MaxValue);
        }
    }
}