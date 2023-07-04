// <copyright file="MenuTreeHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace OneFrameCGPTPlayground.Application.Helpers
{
    /// <summary>
    /// MenuTreeHelper.
    /// </summary>
    public static class MenuTreeHelper
    {
        /// <summary>
        /// Gets the menu tree for tree plugin.
        /// </summary>
        /// <param name="menuList">The menu list.</param>
        /// <returns>List[MenuTreeViewItemDto].</returns>
        public static List<MenuTreeViewItemDto> GetMenuTree(List<MenuDto> menuList)
        {
            List<MenuTreeViewItemDto> resultList = new List<MenuTreeViewItemDto>();

            var list = menuList.Select(x =>
            {
                return new MenuTreeViewItemDto
                {
                    Id = x.Id.ToString(),
                    ParentId = x.ParentId.ToString(),
                    Text = x.DisplayText,
                    OrderId = x.OrderId,
                    State = new MenuTreeViewItemStateInfoDto
                    {
                        Opened = true,
                        Disabled = false,
                        Selected = false,
                    },
                };
            }).ToList();

            list.ForEach(x =>
            {
                x.Children = list.Where(y => y.ParentId != null && y.ParentId == x.Id).ToList();

                if (string.IsNullOrEmpty(x.ParentId))
                {
                    resultList.Add(x);
                }
            });

            return resultList;
        }
    }
}
