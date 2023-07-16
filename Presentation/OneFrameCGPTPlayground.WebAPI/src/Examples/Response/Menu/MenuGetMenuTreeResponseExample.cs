// <copyright file="MenuGetMenuTreeResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.Menu;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class MenuGetMenuTreeResponseExample : IExamplesProvider<ServiceResponse<List<MenuTreeViewItem>>>
    {
        public ServiceResponse<List<MenuTreeViewItem>> GetExamples()
        {
            return new ServiceResponse<List<MenuTreeViewItem>>(new List<MenuTreeViewItem>
            {
                new MenuTreeViewItem
                {
                    Id = "1",
                    State = new MenuTreeViewItemStateInfo
                    {
                        Disabled = false,
                        Opened = true,
                        Selected = true,
                    },
                    Text = "Menu Name 1",
                    Children = new List<MenuTreeViewItem>
                    {
                        new MenuTreeViewItem
                        {
                            Id = "2",
                            State = new MenuTreeViewItemStateInfo
                            {
                                Disabled = false,
                                Opened = true,
                                Selected = true,
                            },
                            Children = null,
                            Text = "Menu Name 2"
                        },
                    },
                },
            });
        }
    }
}
