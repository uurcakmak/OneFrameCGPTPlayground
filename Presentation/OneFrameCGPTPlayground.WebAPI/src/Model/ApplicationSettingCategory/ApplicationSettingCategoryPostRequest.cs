﻿// <copyright file="ApplicationSettingCategoryPostRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.ApplicationSettingCategory
{
    public class ApplicationSettingCategoryPostRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
