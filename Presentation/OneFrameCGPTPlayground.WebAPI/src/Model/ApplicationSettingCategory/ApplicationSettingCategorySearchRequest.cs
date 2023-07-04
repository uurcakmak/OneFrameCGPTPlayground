// <copyright file="ApplicationSettingCategorySearchRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Paging;

namespace OneFrameCGPTPlayground.WebAPI.Model.ApplicationSettingCategory
{
    public class ApplicationSettingCategorySearchRequest : PagedRequest
    {
        public string Name { get; set; }
    }
}
