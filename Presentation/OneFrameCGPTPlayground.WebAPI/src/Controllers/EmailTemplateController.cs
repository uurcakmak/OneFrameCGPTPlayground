// <copyright file="EmailTemplateController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Notification;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("email-templates")]
    [Produces(MediaTypeNames.Application.Json)]
    public class EmailTemplateController : BaseController
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IMapper _mapper;

        public EmailTemplateController(IEmailTemplateService emailTemplateService, IEmailNotificationService emailNotificationService, IMapper mapper)
        {
            _emailTemplateService = emailTemplateService;
            _emailNotificationService = emailNotificationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns email template list.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching email templates with paging properties.</param>
        /// <returns>Returns list of EmailTemplate items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<EmailTemplateListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmailTemplateGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingModel = _mapper.Map<PagedRequest, PagedRequestDto>(pagedRequest);

            var result = await _emailTemplateService.GetEmailTemplateListAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var emailTemplateGetResponses = _mapper.Map<PagedResultDto<EmailTemplateDto>, PagedResult<EmailTemplateListResponse>>(result.Result);

            return Ok(Success(emailTemplateGetResponses));
        }

        /// <summary>
        /// Returns email template by id.
        /// </summary>
        /// <param name="id">Email Template Id</param>
        /// <returns>Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateList)]
        [ProducesResponseType(typeof(ServiceResponse<EmailTemplateListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmailTemplateGetByIdResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _emailTemplateService.GetEmailTemplateByIdAsync(id).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var emailTemplateDto = result.Result;
            var emailTemplateModel = _mapper.Map<EmailTemplateGetWithTranslatesResponse>(emailTemplateDto);
            return Ok(Success(emailTemplateModel));
        }

        /// <summary>
        /// Update email template.
        /// </summary>
        /// <param name="emailTemplate">EmailTemplatePutRequest Model</param>
        /// <returns>Returns EmailTemplateModel. Returns http status codes(200,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateEdit)]
        [SwaggerRequestExample(typeof(EmailTemplatePutRequest), typeof(EmailTemplatePutRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<EmailTemplateListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmailTemplatePutResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutAsync([FromBody] EmailTemplatePutRequest emailTemplate)
        {
            var result = await _emailTemplateService.UpdateEmailTemplateAsync(_mapper.Map<EmailTemplatePutRequest, EmailTemplateDto>(emailTemplate)).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(Success(result.Result));
        }

        /// <summary>
        /// Send example email template.
        /// </summary>
        /// <param name="sendMailRequest">sendTryMailRequest</param>
        /// <returns>Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("send-email")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateEdit)]
        [SwaggerRequestExample(typeof(SendEmailRequest), typeof(EmailTemplateSendTryMailRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailRequest sendMailRequest)
        {
            await _emailNotificationService.SendEmailAsync(sendMailRequest.Subject, sendMailRequest.Content, sendMailRequest.To).ConfigureAwait(false);

            return Ok(Success());
        }

        /// <summary>
        /// Search a email template by application setting key.
        /// </summary>
        /// <param name="emailTemplateGetRequest">Request object for fetching email templates with paging properties.</param>
        /// <returns>Returns found Email Template list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateList)]
        [SwaggerRequestExample(typeof(EmailTemplateSearchRequest), typeof(EmailTemplateSearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<EmailTemplateListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(EmailTemplateSearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] EmailTemplateSearchRequest emailTemplateGetRequest)
        {
            var mappingModel = _mapper.Map<EmailTemplateSearchDto>(emailTemplateGetRequest);
            var result = await _emailTemplateService.SearchAsync(mappingModel).ConfigureAwait(false);
            var emailTemplateGetResponse = _mapper.Map<PagedResultDto<EmailTemplateDto>, PagedResult<EmailTemplateListResponse>>(result.Result);
            return Ok(Success(emailTemplateGetResponse));
        }
    }
}