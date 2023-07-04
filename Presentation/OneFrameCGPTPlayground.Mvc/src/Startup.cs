// <copyright file="Startup.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Common.Helpers;
using OneFrameCGPTPlayground.Common.Helpers.ApplicationSetting;
using OneFrameCGPTPlayground.Infrastructure.Extensions;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Captcha;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Client;
using OneFrameCGPTPlayground.Mvc.Filters;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Jwt.AuthenticationSettings;
using OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenIssuerSettings;
using OneFrameCGPTPlayground.Mvc.Jwt.JwtTokenValidationSettings;
using KocSistem.OneFrame.Common.Cache.Configs;
using KocSistem.OneFrame.Common.Proxy;
using KocSistem.OneFrame.ErrorHandling.ExceptionHandling.ExceptionandlerOptions;
using KocSistem.OneFrame.ErrorHandling.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Globalization;
using System.Net.Http.Headers;

namespace OneFrameCGPTPlayground.Mvc
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            CurrentEnvironment = env;
        }

        private IConfiguration Configuration { get; }

        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string errorPath = new PathString(Configuration["ErrorConfiguration:ErrorPath"]);
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
            }
            else
            {
                _ = app.UseExceptionHandler(errorPath);
                _ = app.UseHsts();
            }

            _ = app.UseStatusCodePagesWithReExecute(errorPath);

            _ = app.UseMvcKsExceptionHandler(new MvcExceptionHandlerOptions
            {
                IsLoggingEnabled = true,
                Path = errorPath
            });

            if (!env.IsDevelopment())
            {
                _ = app.UseHttpsRedirection();
            }

            _ = app.UseStaticFiles();

            _ = app.UseRouting();

            _ = app.UseAuthentication();
            _ = app.UseAuthorization();

            _ = app.UseRequestLocalization();

            _ = app.UseEndpoints(endpoints =>
              {
                  _ = endpoints.MapHealthChecks(Configuration["Monitoring:HealthCheck:Path"], new HealthCheckOptions
                  {
                      Predicate = _ => true,
                      ResponseWriter = HealthCheckResponseWriter.HealthCheckResponse,
                  });

                  _ = endpoints.MapDefaultControllerRoute();
              });
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "ASP0000:Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'", Justification = "BuildServiceProvider")]
        public void ConfigureServices(IServiceCollection services)
        {
            HealthCheckConfigurations(services);

            _ = services.AddControllersWithViews();

            _ = services.Configure<JwtTokenValidationSettingModel>(Configuration.GetSection(nameof(JwtTokenValidationSettingModel)));
            _ = services.AddSingleton<IJwtTokenValidationSettings, JwtTokenValidationSettingsFactory>();

            _ = services.Configure<JwtTokenIssuerSettingModel>(Configuration.GetSection(nameof(JwtTokenIssuerSettingModel)));
            _ = services.AddSingleton<IJwtTokenIssuerSettings, JwtTokenIssuerSettingsFactory>();

            _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            _ = services.Configure<AuthenticationSettingModel>(Configuration.GetSection(nameof(AuthenticationSettingModel)));
            _ = services.AddSingleton<IAuthenticationSettings, AuthenticationSettingsFactory>();

            _ = services.AddScoped<IHtmlHelper, HtmlHelper>();
            _ = services.AddTransient<ICaptchaValidator, GoogleReCaptchaValidator>();
            _ = services.AddTransient<IClientProxy, ClientProxy>();
            _ = services.AddTransient<IProxyHelper, ProxyHelper>();
            _ = services.AddSingleton<HtmlMinSuffixHelper>();
            _ = services.AddScoped<HtmlRtlSuffixHelper>();
            _ = services.AddTransient<IClaimHelper, ClaimHelper>();

            var serviceProvider = services.BuildServiceProvider();

            services.AddKsI18NServices("wwwroot/resources/", true);
            services.AddAuthorizationServices();
            services.AddDistributedCacheServices(Configuration);

            var authenticationSettings = serviceProvider.GetService<IAuthenticationSettings>();
            _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddCookie(JwtBearerDefaults.AuthenticationScheme, options =>
                         {
                             options.LoginPath = authenticationSettings.LoginPath;
                             options.AccessDeniedPath = authenticationSettings.AccessDeniedPath;
                             options.Cookie.SameSite = SameSiteMode.Strict;
                         });
            AddMvcCore(services);

            var builder = services.AddRazorPages();

            if (!CurrentEnvironment.IsDevelopment())
            {
                _ = services.AddHttpsRedirection(options =>
                  {
                      options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                      options.HttpsPort = Convert.ToInt32(Configuration["HostingSettings:HttpsPort"], CultureInfo.InvariantCulture);
                  });
            }
            else
            {
                _ = builder.AddRazorRuntimeCompilation();
            }

            _ = services.Configure<RazorViewEngineOptions>(options =>
              {
                  options.ViewLocationExpanders.Add(new ViewLocationExpander());
              });
            _ = services.AddHttpClient("jwtIssuerClient", httpClient =>
              {
                  httpClient.BaseAddress = new Uri(serviceProvider.GetService<IConfiguration>()["Identity:Jwt:IssuerSettings:BaseAddress"]);
                  httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              });

            _ = services.AddSingleton<IApplicationSettingConfig>(new ApplicationSettingConfig { CategoryNameList = { ConfigurationCategoryConstant.SystemShared, ConfigurationCategoryConstant.SystemMvcUi } });
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
                      _ = options.Filters.Add(typeof(CultureFilter));
                  })
                .AddXmlSerializerFormatters()
                .AddDataAnnotationsLocalization();
        }

        private void HealthCheckConfigurations(IServiceCollection services)
        {
            var healthChecksBuilder = services.AddHealthChecks().AddUrlGroup(
                uri: new Uri(Configuration["Monitoring:Services:Default:Api:Uri"]),
                name: Configuration["Monitoring:Services:Default:Api:Name"],
                failureStatus: (HealthStatus)Enum.Parse(typeof(HealthStatus), Configuration["Monitoring:Services:Default:Api:FailureStatus"]),
                timeout: new TimeSpan(0, 0, 10));

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
    }
}