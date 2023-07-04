// <copyright file="ApplicationSettingCategoryController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.ApplicationSettingCategory;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.Paging;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("application-setting-categories")]
    public class ApplicationSettingCategoryController : BaseController<ApplicationSettingCategoryController>
    {
        private readonly IKsStringLocalizer<ApplicationSettingCategoryController> _localize;

        public ApplicationSettingCategoryController(IKsI18N i18N)
             : base(i18N)
        {
            _localize = i18N.GetLocalizer<ApplicationSettingCategoryController>();
        }

        [HttpGet("create")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryCreate)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryDelete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await DeleteApiRequestAsync<ServiceResponse>(ApiEndpoints.ApplicationSettingCategoryBaseRoute + id).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["DeleteApplicationSettingCategorySuccess"], MvcEndpoints.ApplicationSettingCategoryBaseRoute);
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        public async Task<IActionResult> GetAppSettingCategoryInfoAsync(Guid id)
        {
            var response = await GetApiRequestAsync<ServiceResponse<ApplicationSettingCategoryViewModel>>(string.Format(ApiEndpoints.ApplicationSettingCategoryGetById, id)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return PartialView(response.Result);
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        public async Task<IActionResult> GetListAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var applicationSettingCategorySearchRequest = GetApplicationSettingCategorySearchRequest(request);
                return await SearchAsync(applicationSettingCategorySearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiAppSettingCategoryGetResponseModel>>>(ApiEndpoints.ApplicationSettingCategoryPagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryCreate)]
        public async Task<IActionResult> PostAsync(ApplicationSettingCategoryPostModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.ApplicationSettingCategoryBaseRoute, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["CreateApplicationSettingCategorySuccess"], MvcEndpoints.ApplicationSettingCategoryBaseRoute);
        }

        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryEdit)]
        public async Task<IActionResult> PutAsync(AppSettingCategoryPutViewModel model)
        {
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.ApplicationSettingCategoryBaseRoute, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["UpdateApplicationSettingCategorySuccess"], MvcEndpoints.ApplicationSettingCategoryBaseRoute);
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementApplicationSettingCategoryList)]
        public async Task<IActionResult> SearchAsync([FromQuery] ApplicationSettingCategorySearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiAppSettingCategoryGetResponseModel>>>(ApiEndpoints.ApplicationSettingCategorySearch, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        private static ApplicationSettingCategorySearchRequest GetApplicationSettingCategorySearchRequest(DataTablesRequest requestModel)
        {
            var applicationSettingCategorySearchRequest = new ApplicationSettingCategorySearchRequest()
            {
                Name = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return applicationSettingCategorySearchRequest;
        }
    }
}