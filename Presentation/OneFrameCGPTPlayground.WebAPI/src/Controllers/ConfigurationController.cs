// <copyright file="ConfigurationController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.WebAPI.Model.Configuration;
using OneFrameCGPTPlayground.WebAPI.Model.FileUpload;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Net.Mime;
using TimeZoneNames;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("configurations")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ConfigurationController : BaseController
    {
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly FileUploaderConfigurationOptions _options;

        public ConfigurationController(IOptions<FileUploaderConfigurationOptions> options, IApplicationSettingService applicationSettingService)
        {
            _applicationSettingService = applicationSettingService;
            if (options != null)
            {
                _options = options.Value;
            }
        }

        /// <summary>
        /// Call the configuration options created for the file upload component.
        /// </summary>
        /// <returns>Returns configuration options for file upload. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("file-uploader")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<FileUploaderConfigurationOptions>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ConfigurationGetFileUploaderOptionsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<ActionResult> GetFileUploaderOptionsAsync()
        {
            var result = _options;

            return await Task.Run(() => Ok(Success(result))).ConfigureAwait(false);
        }

        /// <summary>
        /// Call the configuration values.
        /// </summary>
        /// <returns>Returns configuration options for file upload. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("mvc-ui")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse<Dictionary<string, dynamic>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ConfigurationGetDatabaseValuesResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<ActionResult> GetMvcUiConfigurationsAsync([FromBody] List<string> key)
        {
            return await GetConfigurationByCategoryAsync(key, new List<string> { ConfigurationCategoryConstant.SystemMvcUi }).ConfigureAwait(false);
        }

        /// <summary>
        /// Call the configuration values.
        /// </summary>
        /// <returns>Returns configuration options for file upload. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("react")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse<Dictionary<string, dynamic>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ConfigurationGetDatabaseValuesResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<ActionResult> GetReactConfigurationsAsync([FromBody] List<string> key)
        {
            return await GetConfigurationByCategoryAsync(key, new List<string> { ConfigurationCategoryConstant.SystemReact }).ConfigureAwait(false);
        }

        /// <summary>
        /// Call the configuration values.
        /// </summary>
        /// <returns>Returns configuration options for file upload. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("react-startup")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse<Dictionary<string, dynamic>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ConfigurationGetDatabaseValuesResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<ActionResult> GetReactStartupConfigurationsAsync()
        {
            return await GetConfigurationByCategoryAsync(new List<string> { ConfigurationCategoryConstant.SystemReact }).ConfigureAwait(false);
        }

        [HttpGet("time-zones")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse<List<TimeZoneResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TimeZoneResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public Task<ActionResult> GetTimeZones()
        {
            var result = new List<TimeZoneResponse>();

            foreach (var timeZone in TZNames.GetDisplayNames(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, true))
            {
                result.Add(new TimeZoneResponse
                {
                    DisplayName = timeZone.Value,
                    Id = timeZone.Key
                });
            }

            return Task.FromResult<ActionResult>(Ok(Success(result)));
        }

        private async Task<ActionResult> GetConfigurationByCategoryAsync(List<string> categoryNameList)
        {
            var categoryList = new List<string> { ConfigurationCategoryConstant.SystemShared };
            categoryList.AddRange(categoryNameList);

            var data = await _applicationSettingService.GetListByCategoryAsync(categoryList).ConfigureAwait(false);

            return Ok(Success(data.Result));
        }

        private async Task<ActionResult> GetConfigurationByCategoryAsync(List<string> keyList, List<string> categoryNameList)
        {
            var categoryList = new List<string> { ConfigurationCategoryConstant.SystemShared };
            categoryList.AddRange(categoryNameList);

            var data = await _applicationSettingService.GetByKeyAsync(keyList, categoryList).ConfigureAwait(false);

            return Ok(Success(data.Result));
        }
    }
}