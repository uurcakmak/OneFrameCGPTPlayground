// <copyright file="Startup.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Audit.Core;
using Audit.WebApi;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Common.Extensions;
using OneFrameCGPTPlayground.Common.Helpers;
using OneFrameCGPTPlayground.Common.Helpers.ApplicationSetting;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;
using OneFrameCGPTPlayground.Domain;
using OneFrameCGPTPlayground.Infrastructure.Extensions;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Captcha;
using OneFrameCGPTPlayground.Persistence;
using OneFrameCGPTPlayground.Persistence.Contexts.Main;
using OneFrameCGPTPlayground.WebAPI.Filters;
using OneFrameCGPTPlayground.WebAPI.Helper;
using OneFrameCGPTPlayground.WebAPI.Model.FileUpload;
using KocSistem.OneFrame.Authentication.Interfaces;
using KocSistem.OneFrame.Authentication.Services;
using KocSistem.OneFrame.Common.Cache.Configs;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data.Relational.Contract.Options;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.ErrorHandling;
using KocSistem.OneFrame.ErrorHandling.ExceptionHandling.ExceptionandlerOptions;
using KocSistem.OneFrame.ErrorHandling.Web;
using KocSistem.OneFrame.Logging;
using KocSistem.OneFrame.Notification.Sms.JetSms;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace OneFrameCGPTPlayground.WebAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            CurrentEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public static IServiceProvider ServiceProvider { get; private set; }

        private IConfiguration Configuration { get; set; }

        private IWebHostEnvironment CurrentEnvironment { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.UseApiKsExceptionHandler(new ApiExceptionHandlerOptions
            {
                IsLoggingEnabled = true,
            });

            _ = app.UseStatusCodePages();

            _ = app.UseStaticFiles();

            _ = app.UseRouting();

            _ = app.UseCors("AllowAll");

            _ = app.UseAuthentication();
            _ = app.UseAuthorization();

            _ = app.UseRequestLocalization();

            if (!env.IsProduction())
            {
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c =>
                  {
                      c.SwaggerEndpoint(StartupPaths.EndpointUrl, "My API V1");
                      c.RoutePrefix = string.Empty;
                      c.InjectStylesheet(StartupPaths.StylesheetPath);
                      c.InjectJavascript(StartupPaths.JavascriptPath);
                      c.DocExpansion(DocExpansion.None);
                  });
            }

            _ = app.UseEndpoints(endpoints =>
              {
                  _ = endpoints.MapHealthChecks(Configuration["Monitoring:HealthCheck:Path"], new HealthCheckOptions
                  {
                      Predicate = _ => true,
                      ResponseWriter = HealthCheckResponseWriter.HealthCheckResponse,
                  });

                  _ = endpoints.MapControllers();
              });

            app.UseForwardedHeaders();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "ASP0000:Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'", Justification = "BuildServiceProvider")]
        public void ConfigureServices(IServiceCollection services)
        {
            HealthCheckConfigurations(services);

            _ = services.AddControllers();

            _ = services.AddKsLogging();

            services.RegisterApplicationServices();

            services.AddPersistenceInfrastructure(Configuration);

            _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            _ = services.AddTransient<IServiceResponseHelper, ServiceResponseHelper>();

            _ = services.AddTransient<ICaptchaValidator, GoogleReCaptchaValidator>();

            _ = services.AddTransient<ITokenBuilderService, TokenBuilderService>();

            _ = services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();

            _ = services.Configure<FileUploaderConfigurationOptions>(Configuration.GetSection("ComponentSettings:FileUploadConfigurationOptions"));

            if (!CurrentEnvironment.IsProduction())
            {
                AddSwaggerDocumentation(services);
            }

            services.AddEmailNotificationsServices(Configuration);
            services.AddKsI18NServices("resources/", false);
            services.AddIdentityServices(Configuration);
            services.AddAuthorizationServices();
            services.AddDistributedCacheServices(Configuration);
            services.AddCorsServices();

            AddAuthentication(services);

            _ = services.AddErrorHandling();

            AddMvcCore(services);

            var serviceProvider = services.BuildServiceProvider();

            _ = services.AddLogging(loggingBuilder =>
              {
                  _ = loggingBuilder.AddDebug();
              });

            SetAuditDisabled();
            AddAudit(services);

            if (CurrentEnvironment.IsDevelopment())
            {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<MainDbContext>();
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    dbContext.Database.Migrate();
                }
            }

            _ = services.AddHttpClient();

            _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            _ = services.AddTransient(provider =>
              {
                  using var scope = serviceProvider.CreateScope();
                  var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
                  var result = new EntityAuditingOptions() { UserId = httpContextAccessor.HttpContext.User.GetUserId() };
                  return Options.Create(result);
              });

            AddTimeZone(services, serviceProvider);

            _ = services.AddJetSmsNotification(new JetSmsNotificationSettings()
            {
                Username = Configuration["NotificationSettings:SMS:UserName"],
                Password = Configuration["NotificationSettings:SMS:Password"],
                ProviderUrl = new Uri(Configuration["NotificationSettings:SMS:ProviderUrl"]),
            });

            _ = services.AddSingleton<IApplicationSettingConfig>(new ApplicationSettingConfig { CategoryNameList = { ConfigurationCategoryConstant.SystemShared, ConfigurationCategoryConstant.SystemWebApi } });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            ServiceProvider = serviceProvider;
        }

        private static void AddMvcCore(IServiceCollection services)
        {
            // https://github.com/aspnet/Mvc/blob/master/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            _ = services.AddMvc((options) =>
              {
                  options.RespectBrowserAcceptHeader = true;
                  options.ReturnHttpNotAcceptable = true;
                  options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
                  options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                  _ = options.Filters.Add(typeof(ValidateModelAttribute));
              })
                .AddXmlSerializerFormatters()
                .AddDataAnnotationsLocalization();
        }

        private static void AddSwaggerDocumentation(IServiceCollection services)
        {
            _ = services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Default API", Version = "v1" });

                  c.ExampleFilters();

                  c.OperationFilter<AddResponseHeadersFilter>();

                  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                  {
                      Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                      Name = "Authorization",
                      In = ParameterLocation.Header,
                      Type = SecuritySchemeType.ApiKey,
                      Scheme = "Bearer",
                  });

                  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                  });
                  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                  c.IncludeXmlComments(xmlPath);
              }).AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        private static void AddTimeZone(IServiceCollection services, ServiceProvider serviceProvider)
        {
            _ = services.AddTransient(provider =>
            {
                using var scope = serviceProvider.CreateScope();
                var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
                var requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();

                var timezoneStr = httpContextAccessor.HttpContext.Request.Cookies[CookieHelper.TimeZone];
                if (string.IsNullOrEmpty(timezoneStr))
                {
                    timezoneStr = httpContextAccessor.HttpContext.Request.Headers[CookieHelper.TimeZone];
                }

                if (string.IsNullOrEmpty(timezoneStr))
                {
                    timezoneStr = System.Web.HttpUtility.ParseQueryString(httpContextAccessor.HttpContext.Request.QueryString.Value).Get(CookieHelper.TimeZone);
                }

                var timeZone = string.IsNullOrEmpty(timezoneStr) ? TimeZoneInfo.Utc : (DateTimeExtensions.GetTimeZoneInfo(timezoneStr) ?? TimeZoneInfo.Utc);

                var result = new UserLocalizationSettings() { CultureInfo = requestCulture.RequestCulture.Culture, TimeZone = timeZone };
                return Options.Create(result);
            });
        }

        private void HealthCheckConfigurations(IServiceCollection services)
        {
            var healthChecksBuilder = services.AddHealthChecks().AddDbContextCheck<MainDbContext>("MainDbContext", tags: new string[] { "db" }, failureStatus: HealthStatus.Degraded);

            var cacheEnabled = Convert.ToBoolean(Configuration["CacheSettings:Enabled"], CultureInfo.GetCultureInfo("en-US"));

            if (cacheEnabled)
            {
                if (!Enum.TryParse<CacheProvider>(Configuration["CacheSettings:Provider"], out var provider))
                {
                    provider = CacheProvider.None;
                }

                if (provider != CacheProvider.None)
                {
                    switch (provider)
                    {
                        case CacheProvider.SqlServerCache:
                            healthChecksBuilder.AddSqlServer(
                                connectionString: Configuration["CacheSettings:SqlServerCache:ConnectionString"],
                                healthQuery: $"SELECT 1 FROM {Configuration["CacheSettings:SqlServerCache:SchemaName"]}.{Configuration["CacheSettings:SqlServerCache:TableName"]};",
                                name: "sqlCacheDependency",
                                failureStatus: HealthStatus.Degraded,
                                tags: new string[] { "cache", "sql", "sqlserver", "sqlCacheDependency" });
                            break;

                        case CacheProvider.RedisCache:
                            healthChecksBuilder.AddRedis(
                                redisConnectionString: Configuration["CacheSettings:RedisCache:Configuration"],
                                name: "redisCache",
                                failureStatus: HealthStatus.Degraded,
                                tags: new string[] { "cache", "redis" });
                            break;
                    }
                }
            }
        }

        private void AddAudit(IServiceCollection services)
        {
            _ = services.AddMvc(mvc =>
              {
                  mvc.AddAuditFilter(config => config
                  .LogActionIf(d => d.ControllerName == "User")
                  .WithEventType("{verb}.{controller}.{action}")
                  .IncludeHeaders(ctx => !ctx.ModelState.IsValid)
                  .IncludeRequestBody()
                  .IncludeModelState()
                  .IncludeResponseBody());
              });

            ConfigureAudit();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            if (!int.TryParse(Configuration["Identity:Jwt:ExpireInMinutes"], out var expireInMinutes))
            {
                throw new OneFrameLayerInitializationException("Invalid ExpireInMinutes in web.config");
            }

            _ = services.AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromMinutes(expireInMinutes),
                        ValidIssuer = Configuration["Identity:Jwt:Issuer"],
                        ValidAudience = Configuration["Identity:Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Identity:Jwt:Key"])),
                    };
                }).AddGoogle(options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    options.Scope.Add("profile");
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.Events = new OAuthEvents()
                    {
                        OnRemoteFailure = context =>
                        {
                            context.HandleResponse();
                            var error = context?.Failure?.Message;
                            return Task.FromResult(0);
                        },
                    };
                }).AddFacebook(options =>
                {
                    options.AppId = Configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                }).AddMicrosoftAccount(options =>
                {
                    options.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });
        }

        private void ConfigureAudit()
        {
            _ = Audit.Core.Configuration.Setup()
                .UseSqlServer(config => config
                    .ConnectionString(Configuration["Data:MainDbContext:ConnectionString"])
                    .Schema("dbo")
                    .TableName("EventLog")
                    .IdColumnName("Id")
                    .JsonColumnName("AuditData")
                    .LastUpdatedColumnName("AuditDate")
                    .CustomColumn("Id", ev => Guid.NewGuid())
                    .CustomColumn("AuditDate", ev => DateTime.UtcNow.ToUniversalTime())
                    .CustomColumn("AuditUser", ev => ev.Environment.UserName));
        }

        private void SetAuditDisabled()
        {
            Audit.Core.Configuration.AuditDisabled = !Convert.ToBoolean(Configuration["Audit:Enabled"], CultureInfo.InvariantCulture);
        }
    }
}