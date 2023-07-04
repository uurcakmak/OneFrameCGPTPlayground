// <copyright file="LoginAuditLogController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.LoginAuditLog;
using OneFrameCGPTPlayground.Mvc.Models.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("login-audit-logs")]
    public class LoginAuditLogController : BaseController<LoginAuditLogController>
    {
        private readonly IKsStringLocalizer<LoginAuditLogController> _localize;

        public LoginAuditLogController(IKsI18N i18N)
            : base(i18N)
        {
            _localize = i18N.GetLocalizer<LoginAuditLogController>();
        }

        [HttpPost("export-excel")]
        [Authorize(Policy = KsPermissionPolicy.ManagementExcelExport)]
        public async Task<IActionResult> ExcelExportAsync(LoginAuditLogFilterRequest searchRequest)
        {
            var response = await PostApiRequestWithCookiesAsync<ServiceResponse<ExcelExportResponseModel>>(ApiEndpoints.LoginAuditLogExcelExport, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.Result);
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ReportLoginAuditLogList)]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ReportLoginAuditLogList)]
        public async Task<IActionResult> GetListAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var loginAuditLogSearchRequest = GetLoginAuditLogSearchRequest(request);
                return await SearchAsync(loginAuditLogSearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestWithCookiesAsync<ServiceResponse<PagedResult<LoginAuditLogGetResponseModel>>>(ApiEndpoints.LoginAuditLogPagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpPost("pdf-export")]
        [Authorize(Policy = KsPermissionPolicy.ReportLoginAuditLogList)]
        public async Task<IActionResult> PdfExportAsync(LoginAuditLogFilterRequest searchRequest)
        {
            var response = await PostApiRequestWithCookiesAsync<ServiceResponse<PdfExportResponseModel>>(ApiEndpoints.LoginAuditLogPdfExport, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.Result);
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ReportLoginAuditLogList)]
        public async Task<IActionResult> SearchAsync([FromQuery] LoginAuditLogSearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<LoginAuditLogGetResponseModel>>>(ApiEndpoints.LoginAuditLogSearch, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        private static LoginAuditLogSearchRequest GetLoginAuditLogSearchRequest(DataTablesRequest requestModel)
        {
            var loginAuditLogSearchRequest = new LoginAuditLogSearchRequest()
            {
                Value = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return loginAuditLogSearchRequest;
        }
    }
}