// <copyright file="LoginAuditLogController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.LoginAuditLog;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("login-audit-logs")]
    [Produces(MediaTypeNames.Application.Json)]
    public class LoginAuditLogController : BaseController
    {
        private readonly ILoginAuditLogService _loginAuditLogService;
        private readonly IMapper _mapper;

        public LoginAuditLogController(ILoginAuditLogService loginAuditLogService, IMapper mapper)
        {
            _loginAuditLogService = loginAuditLogService;
            _mapper = mapper;
            _loginAuditLogService = loginAuditLogService;
        }

        /// <summary>
        /// Returns login audit logs.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching login auidt logs with paging properties.</param>
        /// <returns>Returns list of login auidt log. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ReportLoginAuditLogList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LoginAuditLogResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginAuditLogGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingModel = _mapper.Map<PagedRequest, PagedRequestDto>(pagedRequest);

            var result = await _loginAuditLogService.GetLoginAuditLogsAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var loginAuditLogResponse = _mapper.Map<PagedResultDto<LoginAuditLogDto>, PagedResult<LoginAuditLogResponse>>(result.Result);
            return Ok(Success(loginAuditLogResponse));
        }

        /// <summary>
        /// Search a login audit log by login audit log ip.
        /// </summary>
        /// <param name="searchRequest">Request object for fetching login audit logs with paging properties.</param>
        /// <returns>Returns found items list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ReportLoginAuditLogList)]
        [SwaggerRequestExample(typeof(LoginAuditLogSearchRequest), typeof(LoginAuditLogSearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<LoginAuditLogResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginAuditLogSearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] LoginAuditLogSearchRequest searchRequest)
        {
            var mappingModel = _mapper.Map<LoginAuditLogSearchDto>(searchRequest);
            var result = await _loginAuditLogService.SearchAsync(mappingModel).ConfigureAwait(false);
            var loginAuditLogGetResponse = _mapper.Map<PagedResultDto<LoginAuditLogDto>, PagedResult<LoginAuditLogResponse>>(result.Result);
            return Ok(Success(loginAuditLogGetResponse));
        }

        /// <summary>
        /// Search a login audit log by login audit log ip.
        /// </summary>
        /// <param name="searchRequest">LoginAuditLogFilterRequest Model</param>
        /// <returns>Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("export-excel")]
        [Authorize(Policy = KsPermissionPolicy.ManagementExcelExport)]
        [SwaggerRequestExample(typeof(LoginAuditLogFilterRequest), typeof(LoginAuditLogFilterRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<ExcelExportResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginAuditLogFilterResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> ExcelExportAsync([FromBody] LoginAuditLogFilterRequest searchRequest)
        {
            searchRequest.EndDate = searchRequest.EndDate.AddDays(1).AddSeconds(-1);
            var mappingModel = _mapper.Map<LoginAuditLogFilterDto>(searchRequest);
            var result = await _loginAuditLogService.SearchForExcelExportAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(Error(result?.Error?.Message, StatusCodes.Status400BadRequest, true));
            }

            return Ok(Success(result.Result));
        }

        /// <summary>
        /// Search a login audit log by login audit log ip.
        /// </summary>
        /// <param name="searchRequest">LoginAuditLogFilterRequest Model</param>
        /// <returns>Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("pdf-export")]
        [Authorize(Policy = KsPermissionPolicy.ManagementPdfExport)]
        [SwaggerRequestExample(typeof(LoginAuditLogFilterRequest), typeof(LoginAuditLogFilterRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PdfExportResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginAuditLogFilterResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PdfExportAsync([FromBody] LoginAuditLogFilterRequest searchRequest)
        {
            searchRequest.EndDate = searchRequest.EndDate.AddDays(1).AddSeconds(-1);
            var result = await _loginAuditLogService.SearchForPdfExportAsync(_mapper.Map<LoginAuditLogFilterDto>(searchRequest)).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(Error(result?.Error?.Message, StatusCodes.Status400BadRequest, true));
            }

            return Ok(Success(result.Result));
        }
    }
}