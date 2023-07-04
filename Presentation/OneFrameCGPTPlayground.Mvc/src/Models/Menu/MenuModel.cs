// <copyright file="MenuModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Menu
{
    public class MenuModel
    {
        public IList<MenuModel> Children { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public string DisplayText { get; set; }

        public string Url { get; set; }

        public int OrderId { get; set; }
    }
}