// <copyright file="MenuResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Base.Model;
using Newtonsoft.Json;

namespace OneFrameCGPTPlayground.WebAPI.Model.Menu
{
    public class MenuResponse : TreeModel<MenuResponse>
    {
        public string Icon { get; set; }

        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayText { get; set; }

        [JsonIgnore]
        public int? ParentId { get; set; }

        public int OrderId { get; set; }

        public string Url { get; set; }
    }
}