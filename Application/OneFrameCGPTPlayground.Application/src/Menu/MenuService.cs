// <copyright file="MenuService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Menu;
using OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts;
using OneFrameCGPTPlayground.Application.Helpers;
using OneFrameCGPTPlayground.Common.Helpers;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Menu
{
    /// <summary>
    /// MenuService.
    /// </summary>
    /// <seealso cref="IMenuService" />
    public class MenuService : ApplicationCrudServiceAsync<Domain.Menu, MenuDto, int>, IMenuService
    {
        private readonly IClaimManager _claimManager;
        private readonly IKsStringLocalizer<MenuService> _localize;
        private readonly IMapper _mapper;
        private readonly IServiceResponseHelper _serviceResponseHelper;
        private readonly IRepository<Domain.Menu> _menuRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService"/> class.
        /// </summary>
        /// <param name="menuRepository">The menu repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="claimManager">The claim manager.</param>
        /// <param name="serviceResponseHelper">The service response helper.</param>
        /// <param name="localize">The localize.</param>
        public MenuService(IRepository<Domain.Menu> menuRepository, IMapper mapper, IDataManager dataManager, IClaimManager claimManager, IServiceResponseHelper serviceResponseHelper, IKsStringLocalizer<MenuService> localize)
            : base(menuRepository, mapper, dataManager)
        {
            _claimManager = claimManager;
            _serviceResponseHelper = serviceResponseHelper;
            _mapper = mapper;
            _localize = localize;
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// Gets the user menu list.
        /// </summary>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<IList<UserMenuDto>>> GetUserMenuListAsync()
        {
            var menuResponse = await GetListAsync(include: i => i.Include(x => x.Translations), orderBy: o => o.OrderBy(x => x.OrderId)).ConfigureAwait(false);
            var menus = menuResponse.Result;
            var claims = _claimManager.GetClaims();
            var userMenus = menus.Where(w => claims.Any(a => a.Type == w.ClaimType && a.Value == w.ClaimValue) || w.ClaimValue == null).ToArray();
            var userMenuDtos = _mapper.Map<IList<MenuDto>, IList<UserMenuDto>>(userMenus);
            var menuList = userMenuDtos.Where(item => (item.ParentId != null) || (item.ParentId == null && userMenuDtos.Any(x => x.ParentId == item.Id))).ToList();

            var result = BuildMenuTree(menuList);

            return _serviceResponseHelper.SetSuccess(result);
        }

        /// <summary>
        /// Gets the menu tree.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<ServiceResponse<List<MenuTreeViewItemDto>>> GetMenuTreeAsync()
        {
            var menus = await _menuRepository.GetListAsync(include: i => i.Include(x => x.Translations), orderBy: o => o.OrderBy(x => x.OrderId)).ConfigureAwait(false);
            var menuGetResponse = _mapper.Map<List<Domain.Menu>, List<MenuDto>>(menus);

            if (!menuGetResponse.Any())
            {
                return _serviceResponseHelper.SetError<List<MenuTreeViewItemDto>>(null, _localize["MenuNotFound"], StatusCodes.Status204NoContent, true);
            }

            var menuTreeList = MenuTreeHelper.GetMenuTree(menuGetResponse);

            return _serviceResponseHelper.SetSuccess(menuTreeList);
        }

        /// <summary>
        /// Saves the menu order.
        /// </summary>
        /// <param name="saveMenuOrderDto">The model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse> SaveMenuOrderAsync(SaveMenuOrderDto saveMenuOrderDto)
        {
            if (saveMenuOrderDto.MenuList == null || !saveMenuOrderDto.MenuList.Any())
            {
                return _serviceResponseHelper.SetError(_localize["MenuNotFound"], StatusCodes.Status204NoContent, true);
            }

            var menus = await _menuRepository.GetListAsync().ConfigureAwait(false);

            foreach (var item in saveMenuOrderDto.MenuList)
            {
                var menu = menus.FirstOrDefault(x => x.Id == item.Id);

                menu.ParentId = item.ParentId;
                menu.OrderId = item.OrderId;
            }

            await _menuRepository.UpdateRangeAsync(menus).ConfigureAwait(false);

            return _serviceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Builds the menu tree.
        /// </summary>
        /// <param name="fullMenuList">The full menu list.</param>
        /// <returns>IList - UserMenuDto.</returns>
        private static IList<UserMenuDto> BuildMenuTree(IList<UserMenuDto> fullMenuList)
        {
            var menuData = new List<UserMenuDto>();

            fullMenuList.ToList().ForEach(x =>
            {
                x.Children = fullMenuList.Where(y => y.ParentId != null && y.ParentId == x.Id).ToList();
                if (x.ParentId == null)
                {
                    menuData.Add(x);
                }
            });

            return menuData;
        }
    }
}