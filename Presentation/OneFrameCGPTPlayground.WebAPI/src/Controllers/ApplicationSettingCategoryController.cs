// <copyright file="ApplicationSettingCategoryController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.ApplicationSettingCategory;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("application-setting-categories")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ApplicationSettingCategoryController : BaseController
    {
        private readonly IApplicationSettingCategoryService _applicationSettingCategoryService;
        private readonly IMapper _mapper;

        public ApplicationSettingCategoryController(IApplicationSettingCategoryService applicationSettingCategoryService, IMapper mapper)
        {
            _applicationSettingCategoryService = applicationSettingCategoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Remove application setting category.
        /// </summary>
        /// <param name="id">Application Setting Category Id</param>
        /// <returns>Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryDelete)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _applicationSettingCategoryService.DeleteApplicationSettingCategoryAsync(id).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Returns application setting categories for dropdown list.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching application setting categories with paging properties.</param>
        /// <returns>Returns list of ApplicationSettingCategory items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<ApplicationSettingCategoryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingCategoryGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingModel = _mapper.Map<PagedRequest, PagedRequestDto>(pagedRequest);

            var result = await _applicationSettingCategoryService.GetApplicationSettingCategoryListAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var roleGetResponse = _mapper.Map<PagedResultDto<ApplicationSettingCategoryDto>, PagedResult<ApplicationSettingCategoryResponse>>(result.Result);
            return Ok(Success(roleGetResponse));
        }

        /// <summary>
        /// Returns application setting categories for dropdown list.
        /// </summary>
        /// <returns>Returns list of ApplicationSettingCategory items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        [ProducesResponseType(typeof(ServiceResponse<List<ApplicationSettingCategoryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingCategoryListResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _applicationSettingCategoryService.GetListAsync().ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var applicationSettingCategories = result.Result;
            var applicationSettingCategoryListModel = _mapper.Map<IEnumerable<ApplicationSettingCategoryDto>, List<ApplicationSettingCategoryResponse>>(applicationSettingCategories);

            return Ok(Success(applicationSettingCategoryListModel));
        }

        /// <summary>
        /// Returns application setting category by id.
        /// </summary>
        /// <param name="id">Application Setting Category Id</param>
        /// <returns>Returns ApplicationSettingCategoryModel. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationSettingCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingCategoryGetByIdResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _applicationSettingCategoryService.GetApplicationSettingCategoryByIdAsync(id).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var applicationSettingCategoryDto = result.Result;
            var applicationSettingModel = _mapper.Map<ApplicationSettingCategoryDto, ApplicationSettingCategoryResponse>(applicationSettingCategoryDto);
            return Ok(Success(applicationSettingModel));
        }

        /// <summary>
        /// Create application setting category.
        /// </summary>
        /// <param name="applicationSettingCategory">Application Setting Category Model</param>
        /// <returns>Returns http status codes(200,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryCreate)]
        [SwaggerRequestExample(typeof(ApplicationSettingCategoryPostRequest), typeof(ApplicationSettingCategoryPostRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationSettingCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingCategoryPostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PostAsync([FromBody] ApplicationSettingCategoryPostRequest applicationSettingCategory)
        {
            var result = await _applicationSettingCategoryService.CreateApplicationSettingCategoryAsync(_mapper.Map<ApplicationSettingCategoryPostRequest, ApplicationSettingCategoryDto>(applicationSettingCategory)).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var applicationSettingModel = _mapper.Map<ApplicationSettingCategoryDto, ApplicationSettingCategoryResponse>(result.Result);

            return Ok(Success(applicationSettingModel));
        }

        /// <summary>
        /// Update application setting category.
        /// </summary>
        /// <param name="applicationSettingCategory">Application Setting Category Model</param>
        /// <returns>Returns ApplicationRole item.Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryEdit)]
        [SwaggerRequestExample(typeof(ApplicationSettingCategoryPostRequest), typeof(ApplicationSettingCategoryPutRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationSettingCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingCategoryPutResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutAsync([FromBody] ApplicationSettingCategoryPutRequest applicationSettingCategory)
        {
            var result = await _applicationSettingCategoryService.UpdateApplicationSettingCategoryAsync(_mapper.Map<ApplicationSettingCategoryPutRequest, ApplicationSettingCategoryDto>(applicationSettingCategory)).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var applicationSettingModel = _mapper.Map<ApplicationSettingCategoryDto, ApplicationSettingCategoryResponse>(result.Result);

            return Ok(Success(applicationSettingModel));
        }

        /// <summary>
        /// Search a application setting category by application setting category name.
        /// </summary>
        /// <param name="applicationSettingCategoryGetRequest">Request object for fetching application setting category with paging properties.</param>
        /// <returns>Returns found ApplicationSettingCategory list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        [SwaggerRequestExample(typeof(ApplicationSettingCategorySearchRequest), typeof(ApplicationSettingCategorySearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<ApplicationSettingCategoryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ApplicationSettingCategorySearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] ApplicationSettingCategorySearchRequest applicationSettingCategoryGetRequest)
        {
            var mappingModel = _mapper.Map<ApplicationSettingCategorySearchDto>(applicationSettingCategoryGetRequest);
            var result = await _applicationSettingCategoryService.SearchAsync(mappingModel).ConfigureAwait(false);
            var applicationSettingGetResponse = _mapper.Map<PagedResultDto<ApplicationSettingCategoryDto>, PagedResult<ApplicationSettingCategoryResponse>>(result.Result);
            return Ok(Success(applicationSettingGetResponse));
        }
    }
}