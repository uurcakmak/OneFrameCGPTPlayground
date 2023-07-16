// <copyright file="MenuViewComponent.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Client;
using OneFrameCGPTPlayground.Mvc.Models.Menu;

namespace OneFrameCGPTPlayground.Mvc.ViewComponents.Menu
{
    [ViewComponent(Name = "MenuViewComponent")]
    public class MenuViewComponent : ViewComponent
    {
        private readonly IClientProxy _proxyHelper;

        public MenuViewComponent(IClientProxy proxyHelper)
        {
            _proxyHelper = proxyHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync().ConfigureAwait(false);
            return View(items);
        }

        private async Task<IEnumerable<MenuModel>> GetItemsAsync()
        {
            var response = await _proxyHelper.GetApiRequest<ServiceResponse<IEnumerable<MenuModel>>>("menu").ConfigureAwait(false);

            return response.Result;
        }
    }
}
