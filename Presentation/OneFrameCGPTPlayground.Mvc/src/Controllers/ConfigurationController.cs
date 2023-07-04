// <copyright file="ConfigurationController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.Configuration;
using OneFrameCGPTPlayground.Mvc.Models.FileUpload;
using OneFrameCGPTPlayground.Mvc.Models.User;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("configurations")]
    public class ConfigurationController : BaseController<ConfigurationController>
    {
        public ConfigurationController(IKsI18N i18N)
            : base(i18N)
        {
        }

        [HttpGet("{setName}")]
        [Authorize]
        public async Task<IActionResult> GetAsync(string setName)
        {
            List<string> requestList = setName switch
            {
                "auto-logout" => new List<string>
                    {
                        ConfigurationConstant.IdentityAutoLogoutIdleTimeout,
                        ConfigurationConstant.IdentityAutoLogoutDialogTimeout,
                        ConfigurationConstant.IdentityAutoLogoutIsEnabled,
                    },
                _ => new List<string>(),
            };
            var response = await GetConfigurationsAsync(requestList).ConfigureAwait(false);

            return setName switch
            {
                "auto-logout" => Ok(new ConfigurationAutoLogoutModel
                {
                    IdentityAutoLogoutDialogTimeout = response.IdentityAutoLogoutDialogTimeout,
                    IdentityAutoLogoutIdleTimeout = response.IdentityAutoLogoutIdleTimeout,
                    IdentityAutoLogoutIsEnabled = response.IdentityAutoLogoutIsEnabled,
                }),
                _ => Ok(),
            };
        }

        [HttpGet("file-uploader-options")]
        [Authorize]
        public async Task<IActionResult> GetFileUploaderOptionsAsync()
        {
            var response = await GetApiRequestAsync<ServiceResponse<FileUploaderConfigurationOptions>>(ApiEndpoints.ConfigurationFileUploader).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Content(JsonConvert.SerializeObject(response));
        }

        [HttpGet("time-zones")]
        [AllowAnonymous]
        public async Task<List<TimeZoneModel>> GetTimeZonesAsync()
        {
            var response = await GetApiRequestAsync<ServiceResponse<List<TimeZoneModel>>>(ApiEndpoints.ConfigurationTimeZone).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return new List<TimeZoneModel>();
            }

            return response.Result;
        }
    }
}