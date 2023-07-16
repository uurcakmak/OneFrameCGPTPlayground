// <copyright file="EmailNotificationController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Controllers;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.Paging;

namespace OneFrameCGPTPlayground.Mvc
{
    [Route("email-notifications")]
    public class EmailNotificationController : BaseController<EmailNotificationController>
    {
        private readonly IKsStringLocalizer<EmailNotificationController> _ksStringLocalizer;

        public EmailNotificationController(IKsI18N i18N)
             : base(i18N)
        {
            _ksStringLocalizer = i18N.GetLocalizer<EmailNotificationController>();
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ReportEmailNotificationList)]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ReportEmailNotificationList)]
        public async Task<IActionResult> GetListAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var emailNotificationSearchRequest = ConvertToEmailNotificationSearchRequest(request);
                return await SearchAsync(emailNotificationSearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestWithCookiesAsync<ServiceResponse<PagedResult<EmailNotificationGetResponseModel>>>(ApiEndpoints.EmailNotificationPagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ReportEmailNotificationList)]
        public async Task<IActionResult> SearchAsync([FromQuery] EmailNotificationSearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<EmailNotificationGetResponseModel>>>(ApiEndpoints.EmailNotificationSearch, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpGet("send")]
        [Authorize(Policy = KsPermissionPolicy.ReportEmailNotificationSend)]
        public async Task<IActionResult> SendEmailByIdAsync(Guid id)
        {
            var response = await GetApiRequestAsync<ServiceResponse<bool>>(string.Format(ApiEndpoints.EmailNotificationSend, id)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_ksStringLocalizer["EmailNotificationSendSuccess"], MvcEndpoints.EmailNotificationBaseRoute);
        }

        private static EmailNotificationSearchRequest ConvertToEmailNotificationSearchRequest(DataTablesRequest requestModel)
        {
            var emailNotificationSearchRequest = new EmailNotificationSearchRequest()
            {
                Value = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return emailNotificationSearchRequest;
        }
    }
}
