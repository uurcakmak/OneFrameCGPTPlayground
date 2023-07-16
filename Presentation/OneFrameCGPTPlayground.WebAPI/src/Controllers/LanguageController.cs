// <copyright file="LanguageController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Language;
using OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.WebAPI.Examples.Request.Language;
using OneFrameCGPTPlayground.WebAPI.Examples.Response.Language;
using OneFrameCGPTPlayground.WebAPI.Model.Language;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("languages")]
    [Produces(MediaTypeNames.Application.Json)]
    public class LanguageController : BaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IMapper _mapper;

        public LanguageController(ILanguageService languageService, IMapper mapper)
        {
            _languageService = languageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns language list.
        /// </summary>
        /// <returns>Returns list of Language items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<LanguageListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LanguageListDtoExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _languageService.GetListAsync().ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var languageResponses = _mapper.Map<IEnumerable<LanguageDto>, IEnumerable<LanguageListResponse>>(result.Result);

            return Ok(Success(languageResponses));
        }

        /// <summary>
        /// Returns language list.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching languages with paging properties.</param>
        /// <returns>Returns list of Language items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LanguageListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LanguageListDtoExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingModel = _mapper.Map<PagedRequest, PagedRequestDto>(pagedRequest);

            var result = await _languageService.GetLanguageListAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var languageResponses = _mapper.Map<PagedResultDto<LanguageDto>, PagedResult<LanguageListResponse>>(result.Result);

            return Ok(Success(languageResponses));
        }

        /// <summary>
        /// Returns language by id.
        /// </summary>
        /// <param name="id">Language Id</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        [ProducesResponseType(typeof(ServiceResponse<LanguageGetByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LanguageGetByIdResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _languageService.GetByIdAsync(id).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var languageModel = _mapper.Map<LanguageGetByIdResponse>(result.Result);
            return Ok(Success(languageModel));
        }

        /// <summary>
        /// Search a language by key.
        /// </summary>
        /// <param name="languageGetRequest">Request object for fetching languages with paging properties.</param>
        /// <returns>Returns found Language list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageList)]
        [SwaggerRequestExample(typeof(LanguageSearchRequest), typeof(LanguageSearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LanguageListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LanguageSearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] LanguageSearchRequest languageGetRequest)
        {
            var mappingModel = _mapper.Map<LanguageSearchRequest, LanguageSearchDto>(languageGetRequest);
            var result = await _languageService.SearchAsync(mappingModel).ConfigureAwait(false);
            var languageGetResponse = _mapper.Map<PagedResultDto<LanguageDto>, PagedResult<LanguageListResponse>>(result.Result);
            return Ok(Success(languageGetResponse));
        }

        /// <summary>
        /// Create new language.
        /// </summary>
        /// <param name="model">LanguagePostRequest Model</param>
        /// <returns>Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        [SwaggerRequestExample(typeof(LanguagePostRequest), typeof(LanguagePostRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<LanguageResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LanguageResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PostAsync([FromBody] LanguagePostRequest model)
        {
            var mappingModel = _mapper.Map<LanguagePostRequest, LanguageDto>(model);

            var result = await _languageService.CreateAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var languageResponse = _mapper.Map<LanguageDto, LanguageResponse>(result.Result);
            return Ok(Success(languageResponse));
        }

        /// <summary>
        /// Update language.
        /// </summary>
        /// <param name="model">LanguagePutRequest Model</param>
        /// <returns>Returns ApplicationRole item.Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        [SwaggerRequestExample(typeof(LanguagePutRequest), typeof(LanguagePutRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<LanguageResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LanguagePutResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutAsync([FromBody] LanguagePutRequest model)
        {
            var mappingModel = _mapper.Map<LanguagePutRequest, LanguageDto>(model);

            var result = await _languageService.UpdateAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(Success(_mapper.Map<LanguageDto, LanguageResponse>(result.Result)));
        }

        /// <summary>
        /// Remove language.
        /// </summary>
        /// <param name="id">Language Id</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _languageService.DeleteAsync(id).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}