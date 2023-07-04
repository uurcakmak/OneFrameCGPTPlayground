// <copyright file="ServiceCollectionExtensions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Common.Helpers;
using OneFrameCGPTPlayground.Domain;
using OneFrameCGPTPlayground.Infrastructure.Helpers;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Identity;
using OneFrameCGPTPlayground.Persistence.Contexts.Main;
using KocSistem.OneFrame.Caching;
using KocSistem.OneFrame.Caching.Memory;
using KocSistem.OneFrame.Caching.Redis;
using KocSistem.OneFrame.Common.Cache.Configs;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.I18N;
using KocSistem.OneFrame.Notification.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace OneFrameCGPTPlayground.Infrastructure.Extensions
{
    /// <summary>
    /// ServiceCollectionExtensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the authorization services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddAuthorizationServices(this IServiceCollection services)
        {
            _ = services.AddAuthorization(options =>
              {
                  foreach (var propertyInfo in typeof(KsPermissionPolicy).GetFields(BindingFlags.Public | BindingFlags.Static))
                  {
                      if (propertyInfo.CustomAttributes.FirstOrDefault(f => f.AttributeType == typeof(ObsoleteAttribute)) != null)
                      {
                          continue;
                      }

                      var policyValue = propertyInfo.GetValue(null)?.ToString();
                      if (policyValue != null)
                      {
                          options.AddPolicy(policyValue, policy => policy.Requirements.Add(new KsPermissionPolicyRequirement(policyValue)));
                      }
                  }
              });
            _ = services.AddTransient<IAuthorizationHandler, KsPermissionPolicyHandler>();
            _ = services.AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
        }

        /// <summary>
        /// Adds the cors services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddCorsServices(this IServiceCollection services)
        {
            _ = services.AddCors(x =>
              {
                  x.AddPolicy("AllowAll", builder =>
                  {
                      _ = builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                  });
              });
        }

        /// <summary>
        /// Adds the distributed cache services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void AddDistributedCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.ThrowIfNull(nameof(services));

            _ = services.AddSingleton<IClaimManager, ClaimManager>();

            if (!Enum.TryParse<CacheProvider>(configuration["CacheSettings:Provider"], out var provider))
            {
                provider = CacheProvider.None;
            }

            var cacheConfig = new CacheConfig
            {
                Enabled = Convert.ToBoolean(configuration["CacheSettings:Enabled"], CultureInfo.GetCultureInfo("en-US")),
                Provider = provider,
            };

            if (!cacheConfig.Enabled)
            {
                _ = services.AddSingleton<ICacheConfig>(cacheConfig);
                _ = services.AddKsCachingDistributedMemory();
                return;
            }

            switch (cacheConfig.Provider)
            {
                case CacheProvider.SqlServerCache:
                    cacheConfig.DefaultSlidingExpiration = Convert.ToDouble(configuration["CacheSettings:SqlServerCache:DefaultSlidingExpiration"], CultureInfo.GetCultureInfo("en-US").NumberFormat);
                    _ = services.AddKsCachingSqlServer(options =>
                      {
                          options.ConnectionString = configuration["CacheSettings:SqlServerCache:ConnectionString"];
                          options.SchemaName = configuration["CacheSettings:SqlServerCache:SchemaName"];
                          options.TableName = configuration["CacheSettings:SqlServerCache:TableName"];
                          options.DefaultSlidingExpiration = TimeSpan.FromMinutes(cacheConfig.DefaultSlidingExpiration);
                          options.InitializeCacheDatabase = true;
                      });
                    break;

                case CacheProvider.MemoryCache:
                    cacheConfig.DefaultSlidingExpiration = Convert.ToDouble(configuration["CacheSettings:MemoryCache:DefaultSlidingExpiration"], CultureInfo.GetCultureInfo("en-US").NumberFormat);
                    _ = services.AddKsCachingDistributedMemory();
                    break;
                case CacheProvider.RedisCache:
                    _ = services.AddKsCachingRedis(options =>
                    {
                        options.Configuration = configuration["CacheSettings:RedisCache:Configuration"];
                        options.InstanceName = configuration["CacheSettings:RedisCache:InstanceName"];
                    });
                    break;
                default:
                    cacheConfig.Enabled = false;
                    cacheConfig.Provider = CacheProvider.None;
                    _ = services.AddKsCachingDistributedMemory();
                    break;
            }

            _ = services.AddSingleton<ICacheConfig>(cacheConfig);
        }

        /// <summary>
        /// Adds the email notifications services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        public static void AddEmailNotificationsServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddTransient<IEmailNotification, KocSistem.OneFrame.Notification.Email.EmailNotification>();

            _ = services.AddEmailNotifications(new EmailNotificationSettings()
            {
                Host = configuration["NotificationSettings:Email:Host"],
                From = configuration["NotificationSettings:Email:From"],
                Port = Convert.ToInt32(configuration["NotificationSettings:Email:Port"], CultureInfo.InvariantCulture),
                ClientDomain = configuration["NotificationSettings:Email:ClientDomain"],
                DefaultCredentials = true,
                UseSSL = false,
            });
        }

        /// <summary>
        /// Adds the identity services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (!int.TryParse(configuration["Identity:Policy:Password:RequiredUniqueChars"], out var requiredUniqueChars))
            {
                requiredUniqueChars = 4;
            }

            if (!int.TryParse(configuration["Identity:Policy:Password:RequiredLength"], out var requiredLength))
            {
                requiredLength = 8;
            }

            if (!bool.TryParse(configuration["Identity:Policy:Password:RequireDigit"], out var requireDigit))
            {
                requireDigit = true;
            }

            if (!bool.TryParse(configuration["Identity:Policy:Password:RequireLowercase"], out var requireLowercase))
            {
                requireLowercase = true;
            }

            if (!bool.TryParse(configuration["Identity:Policy:Password:RequireUppercase"], out var requireUppercase))
            {
                requireUppercase = true;
            }

            if (!bool.TryParse(configuration["Identity:Policy:Password:RequireNonAlphanumeric"], out var requireNonAlphanumeric))
            {
                requireNonAlphanumeric = true;
            }

            if (!bool.TryParse(configuration["Identity:Policy:SignIn:RequireConfirmedEmail"], out var requireConfirmedEmail))
            {
                requireConfirmedEmail = false;
            }

            if (!bool.TryParse(configuration["Identity:Policy:SignIn:RequireConfirmedPhoneNumber"], out var requireConfirmedPhoneNumber))
            {
                requireConfirmedPhoneNumber = false;
            }

            if (!bool.TryParse(configuration["Identity:Policy:SignIn:RequireConfirmedAccount"], out var requireConfirmedAccount))
            {
                requireConfirmedAccount = false;
            }

            if (!bool.TryParse(configuration["Identity:Policy:Lockout:AllowedForNewUsers"], out var allowedForNewUsers))
            {
                allowedForNewUsers = true;
            }

            if (!int.TryParse(configuration["Identity:Policy:Lockout:DefaultLockoutTimeSpan"], out var defaultLockoutTimeSpan))
            {
                defaultLockoutTimeSpan = 5;
            }

            if (!int.TryParse(configuration["Identity:Policy:Lockout:MaxFailedAccessAttempts"], out var maxFailedAccessAttempts))
            {
                maxFailedAccessAttempts = 5;
            }

            if (!bool.TryParse(configuration["Identity:Policy:User:RequireUniqueEmail"], out var requireUniqueEmail))
            {
                requireUniqueEmail = true;
            }

            var identityBuilder = services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    opt.Password.RequiredUniqueChars = requiredUniqueChars;
                    opt.Password.RequiredLength = requiredLength;
                    opt.Password.RequireDigit = requireDigit;
                    opt.Password.RequireLowercase = requireLowercase;
                    opt.Password.RequireUppercase = requireUppercase;
                    opt.Password.RequireNonAlphanumeric = requireNonAlphanumeric;
                    opt.SignIn.RequireConfirmedEmail = requireConfirmedEmail;
                    opt.SignIn.RequireConfirmedPhoneNumber = requireConfirmedPhoneNumber;
                    opt.SignIn.RequireConfirmedAccount = requireConfirmedAccount;
                    opt.Lockout.AllowedForNewUsers = allowedForNewUsers;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(defaultLockoutTimeSpan);
                    opt.Lockout.MaxFailedAccessAttempts = maxFailedAccessAttempts;
                    opt.User.AllowedUserNameCharacters = configuration["Identity:Policy:User:AllowedUserNameCharacters"];
                    opt.User.RequireUniqueEmail = requireUniqueEmail;
                });
            _ = identityBuilder.AddEntityFrameworkStores<MainDbContext>().AddDefaultTokenProviders()
                .AddPasswordValidator<CustomPasswordValidator<ApplicationUser>>()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>();

            _ = services.AddTransient<ITokenHelper, TokenHelper>();
        }

        /// <summary>
        /// Adds the ks i18 n services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="useCookie">if set to <c>true</c> [use cookie].</param>
        public static void AddKsI18NServices(this IServiceCollection services, string resourcePath, bool useCookie)
        {
            _ = services.AddKsI18N((options) =>
              {
                  options.DefaultCulture = "en-US";
                  options.DefaultUICulture = "en-US";
                  options.PlaceholderFormat = "[[{0}]]";
                  options.ResourcesPath = resourcePath;
                  options.SupportedUICultures = new string[] { "tr-TR", "en-US", "ar-AE" };
                  if (useCookie)
                  {
                      options.KsI18NWebOptions = new KsI18NRequestLocalizationOptions
                      {
                          CookieName = CookieRequestCultureProvider.DefaultCookieName,
                      };
                  }
              });
        }

        /// <summary>
        /// Registers the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            // All service classes using the 'IApplicationService' will be registered automatically.
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
              .Where(t => typeof(IApplicationService).IsAssignableFrom(t) && !t.IsInterface).ToList()
              .ForEach(type =>
              {
                  _ = services.AddTransient(type.GetInterface($"I{type.Name}"), type);
              });
        }
    }
}