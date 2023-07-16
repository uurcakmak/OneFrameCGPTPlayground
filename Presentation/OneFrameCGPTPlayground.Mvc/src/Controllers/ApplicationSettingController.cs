// <copyright file="ApplicationSettingController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.ApplicationSetting;
using OneFrameCGPTPlayground.Mvc.Models.ApplicationSettingCategory;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.Paging;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("application-settings")]
    public class ApplicationSettingController : BaseController<ApplicationSettingController>
    {
        private readonly IKsStringLocalizer<ApplicationSettingController> _localize;

        public ApplicationSettingController(IKsI18N i18N)
            : base(i18N)
        {
            _localize = i18N.GetLocalizer<ApplicationSettingController>();
        }

        [HttpGet("create")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCreate)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingDelete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await DeleteApiRequestAsync<ServiceResponse>(ApiEndpoints.ApplicationSettingBaseRoute + id).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["DeleteApplicationSettingSuccess"], MvcEndpoints.ApplicationSettingBaseRoute);
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingList)]
        public async Task<IActionResult> GetAsync()
        {
            var categories = await GetApiRequestAsync<ServiceResponse<List<ApplicationSettingCategoryViewModel>>>(ApiEndpoints.ApplicationSettingCategoryBaseRoute).ConfigureAwait(false);

            ViewBag.CategoryList = categories.Result;
            return View();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingList)]
        public async Task<IActionResult> GetAppSettingInfoAsync(Guid id)
        {
            var response = await GetApiRequestAsync<ServiceResponse<AppSettingViewModel>>(string.Format(ApiEndpoints.ApplicationSettingGetById, id)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            var categories = await GetApiRequestAsync<ServiceResponse<List<ApplicationSettingCategoryViewModel>>>(ApiEndpoints.ApplicationSettingCategoryBaseRoute).ConfigureAwait(false);

            ViewBag.CategoryList = categories.Result;
            return PartialView(response.Result);
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingList)]
        public async Task<IActionResult> GetListAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var applicationSettingSearchRequest = GetApplicaitonSettingSearchRequest(request);
                return await SearchAsync(applicationSettingSearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiAppSettingGetResponseModel>>>(ApiEndpoints.ApplicationSettingPagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCreate)]
        public async Task<IActionResult> PostAsync(ApplicationSettingPostModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.ApplicationSettingBaseRoute, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["CreateApplicationSettingSuccess"], MvcEndpoints.ApplicationSettingBaseRoute);
        }

        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingEdit)]
        public async Task<IActionResult> PutAsync(AppSettingPutViewModel model)
        {
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.ApplicationSettingBaseRoute, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["UpdateApplicationSettingSuccess"], MvcEndpoints.ApplicationSettingBaseRoute);
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingList)]
        public async Task<IActionResult> SearchAsync([FromQuery] ApplicationSettingSearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiAppSettingGetResponseModel>>>(ApiEndpoints.ApplicationSettingSearch, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        private static ApplicationSettingSearchRequest GetApplicaitonSettingSearchRequest(DataTablesRequest requestModel)
        {
            var applicationSettingSearchRequest = new ApplicationSettingSearchRequest()
            {
                Key = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return applicationSettingSearchRequest;
        }
    }
}