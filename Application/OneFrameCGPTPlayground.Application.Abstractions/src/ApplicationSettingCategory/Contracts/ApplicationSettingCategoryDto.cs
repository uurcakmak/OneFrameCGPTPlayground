// <copyright file="ApplicationSettingCategoryDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory
{
    public class ApplicationSettingCategoryDto : IDto<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ApplicationSettingDto> ApplicationSettings { get; }
    }
}
