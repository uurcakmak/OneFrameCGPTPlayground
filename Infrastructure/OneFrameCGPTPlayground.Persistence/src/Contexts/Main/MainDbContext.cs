// -----------------------------------------------------------------------
// <copyright file="MainDbContext.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Audit.Core;
using Audit.EntityFramework;
using Audit.EntityFramework.Providers;
using KocSistem.OneFrame.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using OneFrameCGPTPlayground.Domain;
using OneFrameCGPTPlayground.Persistence.Data;
using System;
using System.Linq;
using System.Text;

namespace OneFrameCGPTPlayground.Persistence.Contexts.Main
{
    /// <summary>
    /// MainDbContext.
    /// </summary>
    /// <seealso cref="Guid" />
    public class MainDbContext : AuditIdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim, IdentityUserRole, IdentityUserLogin, IdentityRoleClaim, IdentityUserToken>
    {
        /// <summary>
        /// The ef data provider.
        /// </summary>
        private static readonly EntityFrameworkDataProvider EfDataProvider =
        new (_ => _
                                       .AuditTypeMapper(t => typeof(AuditLog))
                                       .AuditEntityAction<AuditLog>((ev, entry, entity) =>
                                       {
                                           entity.AuditData = entry.ToJson();
                                           entity.EntityType = entry.EntityType.Name;
                                           entity.AuditDate = DateTime.Now.ToUniversalTime();
                                           entity.AuditUser = Environment.UserName;
                                           entity.TablePk = entry.PrimaryKey.First().Value.ToString();
                                       })
                                       .IgnoreMatchedProperties(true));

        private readonly IEncryptionProvider _provider;
        private bool _hasOuterTransaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
            AuditDataProvider = EfDataProvider;

            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            var keyByteArray = Encoding.UTF8.GetBytes(key);
            var iv = new byte[16];
            _provider = new AesProvider(keyByteArray, iv);
        }

        public DbSet<TestEncryptionEntity> TestEncryptionEntity { get; set; }

        public DbSet<ApplicationSetting> ApplicationSetting { get; set; }

        public DbSet<ApplicationSettingCategory> ApplicationSettingCategory { get; set; }

        /// <summary>
        /// Called after the audit scope is created.
        /// Override to specify custom logic.
        /// </summary>
        /// <param name="auditScope">The audit scope.</param>
        public override void OnScopeCreated(IAuditScope auditScope)
        {
            if (Database.CurrentTransaction == null)
            {
                _ = Database.BeginTransaction();
            }
            else
            {
                _hasOuterTransaction = true;
            }
        }

        /// <summary>
        /// Called after the EF operation execution and before the AuditScope saving.
        /// Override to specify custom logic.
        /// </summary>
        /// <param name="auditScope">The audit scope.</param>
        public override void OnScopeSaving(IAuditScope auditScope)
        {
            try
            {
                if (auditScope != null)
                {
                    auditScope.Save();
                    auditScope.Discard();
                }
            }
            catch
            {
                if (!_hasOuterTransaction)
                {
                    Database.CurrentTransaction.Rollback();
                }

                throw;
            }

            if (!_hasOuterTransaction)
            {
                Database.CurrentTransaction.Commit();
            }
        }

        /// <summary>
        /// Configures the schema needed for the identity framework.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder.ThrowIfNull(nameof(builder));

            base.OnModelCreating(builder);
            _ = builder.UseEncryption(_provider);

            _ = builder.Entity<ApplicationRole>(ApplicationRoleMappings.OnModelCreating);
            _ = builder.Entity<ApplicationRoleTranslation>(ApplicationRoleTranslationMapping.OnModelCreating);
            _ = builder.Entity<ApplicationUser>(ApplicationUserMappings.OnModelCreating);
            _ = builder.Entity<ApplicationUserPasswordHistory>(ApplicationUserPasswordHistoryMappings.OnModelCreating);
            _ = builder.Entity<UserConfirmationHistory>(UserConfirmationHistoryMappings.OnModelCreating);
            _ = builder.Entity<IdentityRoleClaim>(IdentityRoleClaimMappings.OnModelCreating);
            _ = builder.Entity<IdentityUserClaim>(IdentityUserClaimMappings.OnModelCreating);
            _ = builder.Entity<IdentityUserLogin>(IdentityUserLoginMappings.OnModelCreating);
            _ = builder.Entity<IdentityUserRole>(IdentityUserRoleMappings.OnModelCreating);
            _ = builder.Entity<IdentityUserToken>(IdentityUserTokenMappings.OnModelCreating);
            _ = builder.Entity<Menu>(MenuMappings.OnModelCreating);
            _ = builder.Entity<MenuTranslation>(MenuTranslationMappings.OnModelCreating);
            _ = builder.Entity<AuditLog>(AuditLogMappings.OnModelCreating);
            _ = builder.Entity<EventLog>(EventLogMappings.OnModelCreating);
            _ = builder.Entity<TestEncryptionEntity>(TestEncryptionEntityMappings.OnModelCreating);
            _ = builder.Entity<ApplicationSettingCategory>(ApplicationSettingCategoryMappings.OnModelCreating);
            _ = builder.Entity<ApplicationSetting>(ApplicationSettingMappings.OnModelCreating);
            _ = builder.Entity<LoginAuditLog>(LoginAuditLogMappings.OnModelCreating);
            _ = builder.Entity<EmailTemplate>(EmailTemplateMappings.OnModelCreating);
            _ = builder.Entity<EmailTemplateTranslation>(EmailTemplateTranslationMappings.OnModelCreating);
            _ = builder.Entity<EmailNotification>(EmailNotificationMappings.OnModelCreating);
            _ = builder.Entity<Language>(LanguageMappings.OnModelCreating);
        }
    }
}