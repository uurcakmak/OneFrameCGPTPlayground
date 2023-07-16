// <copyright file="UserController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.ClaimHelper;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.Paging;
using OneFrameCGPTPlayground.Mvc.Models.User;
using System.Globalization;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("users")]
    public class UserController : BaseController<UserController>
    {
        private readonly IKsStringLocalizer<UserController> _localize;

        public UserController(IKsI18N i18N)
        : base(i18N)
        {
            _localize = i18N.GetLocalizer<UserController>();
        }

        [HttpGet("create")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserCreate)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpDelete]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserDelete)]
        public async Task<IActionResult> DeleteAsync(string username)
        {
            var response = await DeleteApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountBaseRoute + username).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["DeleteUserSuccess"], MvcEndpoints.UserBaseRoute);
        }

        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        public async Task<IActionResult> GetAsync([DataTablesRequest] DataTablesRequest request)
        {
            var pagedRequest = GetPagedRequest(request);

            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                var userSearchRequest = GetUserSearchRequest(request);
                return await SearchAsync(userSearchRequest).ConfigureAwait(false);
            }

            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiUserGetResponseModel>>>(ApiEndpoints.AccountPagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("role-assignments/{username}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        public async Task<IActionResult> GetRoleAssignmentsAsync(string username)
        {
            var response = await GetApiRequestAsync<ServiceResponse<List<ApiRoleAssignmentResponse>>>(string.Format(CultureInfo.InvariantCulture, ApiEndpoints.AccountGetRoleAssignments, username)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            var model = new UserRolePutViewModel()
            {
                Roles = response.Result.Select(p => new SelectListItem { Value = p.RoleName, Text = p.RoleName, Selected = p.IsAssigned }).ToList(),
                Username = username
            };
            return PartialView("UserRole", model);
        }

        [HttpGet("{username}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        public async Task<IActionResult> GetUserInfoAsync(string username)
        {
            var response = await GetApiRequestAsync<ServiceResponse<UserViewModel>>(ApiEndpoints.AccountBaseRoute + username).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return View("Get");
            }

            return PartialView(response.Result);
        }

        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserCreate)]
        public async Task<IActionResult> PostAsync(UserViewModel user)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountBaseRoute, user).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["CreateUserSuccess"], MvcEndpoints.UserBaseRoute);
        }

        [HttpPut]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserEdit)]
        public async Task<IActionResult> PutAsync(UserPutViewModel user)
        {
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountBaseRoute + user.Username, user).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["UpdateUserSuccess"], MvcEndpoints.UserBaseRoute);
        }

        [HttpPut("user-roles")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserRole)]
        public async Task<IActionResult> PutUserRoleAsync(UserRolePutViewModel userRole)
        {
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountBaseUserRole, userRole).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["UpdateUserSuccess"], MvcEndpoints.UserBaseRoute);
        }

        [HttpPost("user-claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserAddClaim)]
        public async Task<IActionResult> SaveUserClaimsAsync(SaveUserClaimsModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(string.Format(CultureInfo.InvariantCulture, ApiEndpoints.AccountSaveUserClaims), model, true).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccess(_localize["UpdateUserClaimSuccess"]);
        }

        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        public async Task<IActionResult> SearchAsync([FromQuery] UserSearchRequest searchRequest)
        {
            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<ApiUserGetResponseModel>>>(ApiEndpoints.AccountUserSearch, searchRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(JsonDataTable(response.Result));
        }

        [HttpGet("user-claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserClaimList)]
        public async Task<IActionResult> UserClaimsAsync()
        {
            var pagedRequest = new PagedRequest { PageIndex = 0, PageSize = 100000 };

            var response = await GetApiRequestAsync<ServiceResponse<PagedResult<UserViewModel>>>(ApiEndpoints.AccountPagedList, pagedRequest).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            var model = new UserClaimsViewModel
            {
                UserList = response.Result.Items.Select(r => new SelectListItem { Text = $"{r.Name} {r.Surname} - '{r.Username}'", Value = r.Username, }).ToList(),
            };
            return View(model);
        }

        [HttpGet("user-claims/{userName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserClaimList)]
        public async Task<IActionResult> UserClaimsAsync(string userName)
        {
            var response = await GetApiRequestAsync<ServiceResponse<List<ApiClaimTreeViewItem>>>(string.Format(CultureInfo.InvariantCulture, ApiEndpoints.AccountGetUserClaimsTree, userName)).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.Result);
        }

        private static UserSearchRequest GetUserSearchRequest(DataTablesRequest requestModel)
        {
            var userSearchRequest = new UserSearchRequest()
            {
                Username = requestModel.Search.Value,
                PageIndex = requestModel.Start / requestModel.Length,
                PageSize = requestModel.Length,
            };
            return userSearchRequest;
        }
    }
}