// <copyright file="LanguageController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.Language;
using OneFrameCGPTPlayground.Mvc.Models.Paging;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("languages")]
    public class LanguageController : BaseController<LanguageController>
    {
        private readonly IKsStringLocalizer<LanguageController> _localize;

        public LanguageController(IKsI18N i18N)
            : base(i18N)
        {
            _localize = i18N.GetLocalizer<LanguageController>();
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageList)]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageList)]
        public async Task<IActionResult> GetListAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var languageSearchRequest = GetLanguageSearchRequest(request);
                return await SearchAsync(languageSearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestWithCookiesAsync<ServiceResponse<PagedResult<LanguageGetResponseModel>>>(ApiEndpoints.LanguagePagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingList)]
        public async Task<IActionResult> SearchAsync([FromQuery] LanguageSearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<LanguageGetResponseModel>>>(ApiEndpoints.Search, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        public async Task<IActionResult> GetLanguageInfoAsync(Guid id)
        {
            var response = await GetApiRequestAsync<ServiceResponse<LanguageViewModel>>(string.Format(ApiEndpoints.LanguageGetById, id)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return PartialView(response.Result);
        }

        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        public async Task<IActionResult> PostAsync(LanguagePostModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.LanguageBaseRoute, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
          }

            return ToastSuccessForRedirect(_localize["CreateLanguageSuccess"], MvcEndpoints.LanguageBaseRoute);
        }

        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        public async Task<IActionResult> PutAsync(LanguagePutModel model)
        {
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.LanguageBaseRoute, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["UpdateLanguageSuccess"], MvcEndpoints.LanguageBaseRoute);
        }

        [HttpDelete]
        [Authorize(Policy = KsPermissionPolicy.ManagementLanguageEdit)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await DeleteApiRequestAsync<ServiceResponse>(ApiEndpoints.LanguageBaseRoute + id).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["DeleteLanguageSuccess"], MvcEndpoints.LanguageBaseRoute);
        }

        private static LanguageSearchRequest GetLanguageSearchRequest(DataTablesRequest requestModel)
        {
            var languageSearchRequest = new LanguageSearchRequest()
            {
                Key = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return languageSearchRequest;
        }
    }
}
