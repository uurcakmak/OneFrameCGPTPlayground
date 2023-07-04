// <copyright file="ApplicationSettingController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSetting;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("application-settings")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ApplicationSettingController : BaseController
    {
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly IMapper _mapper;

        public ApplicationSettingController(IApplicationSettingService applicationSettingService, IMapper mapper)
        {
            _applicationSettingService = applicationSettingService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns application setting list.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching application settings with paging properties.</param>
        /// <returns>Returns list of ApplicationSetting items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<ApplicationSettingResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingModel = _mapper.Map<PagedRequest, PagedRequestDto>(pagedRequest);
            var result = await _applicationSettingService.GetListAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var roleGetResponse = _mapper.Map<PagedResultDto<ApplicationSettingDetailDto>, PagedResult<ApplicationSettingResponse>>(result.Result);
            return Ok(Success(roleGetResponse));
        }

        /// <summary>
        /// Returns application setting by id.
        /// </summary>
        /// <param name="id">Application Setting Id</param>
        /// <returns>Returns ApplicationSetting item. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("id/{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationSettingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingGetByIdResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _applicationSettingService.GetByIdAsync(id).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var applicationSettingDto = result.Result;
            var applicationSettingModel = _mapper.Map<ApplicationSettingDto, ApplicationSettingResponse>(applicationSettingDto);
            return Ok(Success(applicationSettingModel));
        }

        /// <summary>
        /// Returns application setting by key.
        /// </summary>
        /// <param name="key">Application Setting Key</param>
        /// <returns>Returns ApplicationSetting item. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{key}")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationSettingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingGetByKeyResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetByKeyAsync(string key)
        {
            var result = await _applicationSettingService.GetByKeyAsync(key).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var applicationSettingModel = _mapper.Map<ApplicationSettingDto, ApplicationSettingResponse>(result.Result);
            return Ok(Success(applicationSettingModel));
        }

        /// <summary>
        /// Create application setting.
        /// </summary>
        /// <param name="applicationSetting">Application Setting Model</param>
        /// <returns>Returns http status codes(200,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCreate)]
        [SwaggerRequestExample(typeof(ApplicationSettingPostRequest), typeof(ApplicationSettingPostRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationSettingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingPostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PostAsync([FromBody] ApplicationSettingPostRequest applicationSetting)
        {
            var result = await _applicationSettingService.CreateAsync(_mapper.Map<ApplicationSettingPostRequest, ApplicationSettingDto>(applicationSetting)).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var applicationSettingGetResponse = _mapper.Map<ApplicationSettingDto, ApplicationSettingResponse>(result.Result);
            return Ok(Success(applicationSettingGetResponse));
        }

        /// <summary>
        /// Update application setting.
        /// </summary>
        /// <param name="applicationSetting">ApplicationSettingPutRequest Model</param>
        /// <returns>Returns ApplicationRole item.Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingEdit)]
        [SwaggerRequestExample(typeof(ApplicationSettingPutRequest), typeof(ApplicationSettingPutRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationSettingResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingPutResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutAsync([FromBody] ApplicationSettingPutRequest applicationSetting)
        {
            var result = await _applicationSettingService.UpdateAsync(_mapper.Map<ApplicationSettingPutRequest, ApplicationSettingDto>(applicationSetting)).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var applicationSettingGetResponse = _mapper.Map<ApplicationSettingDto, ApplicationSettingResponse>(result.Result);
            return Ok(Success(applicationSettingGetResponse));
        }

        /// <summary>
        /// Remove application setting.
        /// </summary>
        /// <param name="applicationSettingId">Application Setting Id</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{applicationSettingId}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingDelete)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> DeleteAsync(Guid applicationSettingId)
        {
            var result = await _applicationSettingService.DeleteAsync(applicationSettingId).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Search a application setting by application setting key.
        /// </summary>
        /// <param name="applicationSettingGetRequest">Request object for fetching application settings with paging properties.</param>
        /// <returns>Returns found ApplicationSetting list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingList)]
        [SwaggerRequestExample(typeof(ApplicationSettingSearchRequest), typeof(ApplicationSettingSearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<ApplicationSettingResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingSearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] ApplicationSettingSearchRequest applicationSettingGetRequest)
        {
            var mappingModel = _mapper.Map<ApplicationSettingSearchDto>(applicationSettingGetRequest);
            var result = await _applicationSettingService.SearchAsync(mappingModel).ConfigureAwait(false);
            var applicationSettingGetResponse = _mapper.Map<PagedResultDto<ApplicationSettingDetailDto>, PagedResult<ApplicationSettingResponse>>(result.Result);
            return Ok(Success(applicationSettingGetResponse));
        }
    }
}