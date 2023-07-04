// <copyright file="RoleController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.ClaimHelper;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.Paging;
using OneFrameCGPTPlayground.Mvc.Models.Role;
using OneFrameCGPTPlayground.Mvc.Models.User;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("roles")]
    public class RoleController : BaseController<RoleController>
    {
        private readonly IKsStringLocalizer<RoleController> _localize;

        public RoleController(IKsI18N i18N)
            : base(i18N)
        {
            _localize = i18N.GetLocalizer<RoleController>();
        }

        [HttpGet("create")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleCreate)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleDelete)]
        public async Task<IActionResult> DeleteAsync(string name)
        {
            var response = await DeleteApiRequestAsync<ServiceResponse>(ApiEndpoints.RoleBaseRoute + name).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["DeleteRoleSuccess"], MvcEndpoints.RoleBaseRoute);
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        public async Task<IActionResult> GetAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var userSearchRequest = GetRoleSearchRequest(request);
                return await SearchAsync(userSearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiRoleGetResponseModel>>>(ApiEndpoints.RolePagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("{name}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        public async Task<IActionResult> GetRoleInfoAsync(string name)
        {
            var response = await GetApiRequestAsync<ServiceResponse<RoleViewModel>>(ApiEndpoints.RoleBaseRoute + name).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return PartialView(response.Result);
        }

        [HttpGet("role-user-info/{name}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        public async Task<IActionResult> GetRoleUserInfoAsync(string name)
        {
            var response = await GetApiRequestAsync<ServiceResponse<List<ApiUserRoleInfoResponseModel>>>(string.Format(CultureInfo.InvariantCulture, ApiEndpoints.RoleGetRoleUserInfo, name)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.Result);
        }

        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleCreate)]
        public async Task<IActionResult> PostAsync(RolePostModel role)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.RoleBaseRoute, role).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["CreateRoleSuccess"], MvcEndpoints.RoleBaseRoute);
        }

        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleEdit)]
        public async Task<IActionResult> PutAsync(RolePutViewModel role)
        {
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.RoleBaseRoute + role.Name, role).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["UpdateRoleSuccess"], MvcEndpoints.RoleBaseRoute);
        }

        [HttpGet("role-claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleClaimList)]
        public async Task<IActionResult> RoleClaimsAsync()
        {
            var pagedRequest = new PagedRequest { PageIndex = 0, PageSize = 100000 };
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<RoleViewModel>>>(ApiEndpoints.RolePagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            var model = new RoleClaimsViewModel
            {
                RoleList = response.Result.Items.Select(r => new SelectListItem { Text = r.Name, Value = r.Name, }).ToList(),
            };
            return View(model);
        }

        [HttpGet("role-claims/{roleName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleClaimList)]
        public async Task<IActionResult> RoleClaimsAsync(string roleName)
        {
            var response = await GetApiRequestAsync<ServiceResponse<List<ApiClaimTreeViewItem>>>(string.Format(CultureInfo.InvariantCulture, ApiEndpoints.RoleGetRoleClaimsTree, roleName)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.Result);
        }

        [HttpPost("role-claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleAddClaim)]
        public async Task<IActionResult> SaveRoleClaimsAsync(SaveRoleClaimsModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(string.Format(CultureInfo.InvariantCulture, ApiEndpoints.RoleSaveRoleClaims), model, true).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccess(_localize["UpdateRoleClaimSuccess"]);
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        public async Task<IActionResult> SearchAsync([FromQuery] RoleSearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiRoleGetResponseModel>>>(ApiEndpoints.RoleSearch, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        private static RoleSearchRequest GetRoleSearchRequest(DataTablesRequest requestModel)
        {
            var roleSearchRequest = new RoleSearchRequest()
            {
                Name = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return roleSearchRequest;
        }
    }
}