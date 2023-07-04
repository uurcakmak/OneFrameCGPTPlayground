// <copyright file="EmailNotificationController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Notification;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("email-notifications")]
    [Produces(MediaTypeNames.Application.Json)]
    public class EmailNotificationController : BaseController
    {
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IMapper _mapper;

        public EmailNotificationController(IEmailNotificationService emailNotificationService, IMapper mapper)
        {
            _emailNotificationService = emailNotificationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns email notificatios.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching email notifications with paging properties.</param>
        /// <returns>Returns list of EmailNotification items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ReportEmailNotificationList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<EmailNotificationResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmailNotificationGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingModel = _mapper.Map<PagedRequest, PagedRequestDto>(pagedRequest);

            var result = await _emailNotificationService.GetEmailNotificationsAsync(mappingModel).ConfigureAwait(false);

            var emailResponse = _mapper.Map<PagedResultDto<EmailNotificationDto>, PagedResult<EmailNotificationResponse>>(result.Result);
            return Ok(Success(emailResponse));
        }

        /// <summary>
        /// Search a email notification by email address.
        /// </summary>
        /// <param name="searchRequest">Request object for fetching email notifications with paging properties.</param>
        /// <returns>Returns found items list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ReportEmailNotificationList)]
        [SwaggerRequestExample(typeof(EmailNotificationSearchRequest), typeof(EmailNotificationSearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<EmailNotificationResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginAuditLogSearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] EmailNotificationSearchRequest searchRequest)
        {
            var mappingModel = _mapper.Map<EmailNotificationSearchRequestDto>(searchRequest);
            var result = await _emailNotificationService.SearchNotificationAsync(mappingModel).ConfigureAwait(false);
            var emailResponse = _mapper.Map<PagedResult<EmailNotificationResponse>>(result.Result);
            return Ok(Success(emailResponse));
        }

        /// <summary>
        /// Send a email notification by Id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns http status codes(200,401,404,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="404">item(s) was not found.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("send/{id}")]
        [Authorize(Policy = KsPermissionPolicy.ReportEmailNotificationSend)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse<bool>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ServiceResponse404Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SendEmailByIdAsync(Guid id)
        {
            var result = await _emailNotificationService.SendEmailByIdAsync(id).ConfigureAwait(false);
            if (!result.IsSuccessful)
            {
                return NotFound(result);
            }

            return Ok(Success(result.Result));
        }
    }
}