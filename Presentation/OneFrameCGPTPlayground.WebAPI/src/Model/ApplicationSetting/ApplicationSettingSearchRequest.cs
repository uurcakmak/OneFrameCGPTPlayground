// <copyright file="ApplicationSettingSearchRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Paging;

namespace OneFrameCGPTPlayground.WebAPI.Model.ApplicationSetting
{
    public class ApplicationSettingSearchRequest : PagedRequest
    {
        public string Key { get; set; }
    }
}
