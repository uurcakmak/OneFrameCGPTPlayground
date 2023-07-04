// <copyright file="IMenuService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.Menu
{
    /// <summary>
    /// IMenuService.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IMenuService : IApplicationCrudServiceAsync<Domain.Menu, MenuDto, int>, IApplicationService
    {
        /// <summary>
        /// Gets the user menu list.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<IList<UserMenuDto>>> GetUserMenuListAsync();

        Task<ServiceResponse<List<MenuTreeViewItemDto>>> GetMenuTreeAsync();

        Task<ServiceResponse> SaveMenuOrderAsync(SaveMenuOrderDto saveMenuOrderDto);
    }
}