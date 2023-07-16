// <copyright file="EmailTemplateController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.EmailTemplate;
using OneFrameCGPTPlayground.Mvc.Models.Paging;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("email-templates")]
    public class EmailTemplateController : BaseController<EmailTemplateController>
    {
        private readonly IKsStringLocalizer<EmailTemplateController> _ksStringLocalizer;

        public EmailTemplateController(IKsI18N i18N)
             : base(i18N)
        {
            _ksStringLocalizer = i18N.GetLocalizer<EmailTemplateController>();
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateList)]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("{id}", Name = "GetEmailTemplateInfo")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateList)]
        public async Task<IActionResult> GetEmailTemplateInfoAsync(Guid id)
        {
            var response = await GetApiRequestAsync<ServiceResponse<EmailTemplateViewModel>>(string.Format(ApiEndpoints.EmailTemplateById, id)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return View(response.Result);
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateList)]
        public async Task<IActionResult> GetListAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var emailTemplateSearchRequest = GetEmailTemplateSearchRequest(request);
                return await SearchAsync(emailTemplateSearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestWithCookiesAsync<ServiceResponse<PagedResult<ApiEmailTemplateGetResponseModel>>>(ApiEndpoints.EmailTemplatePagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateEdit)]
        public async Task<IActionResult> PutAsync(EmailTemplatePutModel model)
        {
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.EmailTemplateBaseRoute, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_ksStringLocalizer["UpdateEmailTemplateSuccess"], MvcEndpoints.EmailTemplateBaseRoute);
        }

        [HttpPost("send-email")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateEdit)]
        public async Task<IActionResult> SendEmailAsync(SendTryMailModel model)
        {
            await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.EmailTemplateSendTryMail, model).ConfigureAwait(false);

            return ToastSuccess(_ksStringLocalizer["SendEmailSuccess"]);
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementEmailTemplateList)]
        public async Task<IActionResult> SearchAsync([FromQuery] EmailTemplateSearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiEmailTemplateGetResponseModel>>>(ApiEndpoints.EmailTemplateSearch, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        private static EmailTemplateSearchRequest GetEmailTemplateSearchRequest(DataTablesRequest requestModel)
        {
            var emailTemplateSearchRequest = new EmailTemplateSearchRequest()
            {
                Name = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return emailTemplateSearchRequest;
        }
    }
}