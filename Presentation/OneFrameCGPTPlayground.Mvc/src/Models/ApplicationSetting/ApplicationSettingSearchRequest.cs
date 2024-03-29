﻿// <copyright file="ApplicationSettingSearchRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Mvc.Models.Paging;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.ApplicationSetting
{
    public class ApplicationSettingSearchRequest : PagedRequest
    {
        [Display(Name = "Key")]
        public string Key { get; set; }
    }
}
