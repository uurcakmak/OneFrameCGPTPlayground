// <copyright file="MenuController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.Menu;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("menus")]
    public class MenuController : BaseController<MenuController>
    {
        private readonly IKsStringLocalizer<MenuController> _localize;

        public MenuController(IKsI18N i18N)
            : base(i18N)
        {
            _localize = i18N.GetLocalizer<MenuController>();
        }

        [HttpGet("tree")]
        [Authorize(Policy = KsPermissionPolicy.ManagementMenuList)]
        public async Task<IActionResult> MenuTreeAsync()
        {
            var response = await GetApiRequestAsync<ServiceResponse<List<ApiMenuTreeViewItem>>>(ApiEndpoints.MenuTree).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.Result);
        }

        [HttpGet("order")]
        [Authorize(Policy = KsPermissionPolicy.ManagementMenuList)]
        public IActionResult MenuOrder()
        {
            return View();
        }

        [HttpPost("order")]
        [Authorize(Policy = KsPermissionPolicy.ManagementMenuEdit)]
        public async Task<IActionResult> SaveMenuOrderAsync(SaveMenuOrderModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.MenuSaveOrder, model, true).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["UpdateMenuOrderSuccess"], MvcEndpoints.MenuOrder);
        }
    }
}
