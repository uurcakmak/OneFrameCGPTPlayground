// <copyright file="ApplicationSettingCategorySearchDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;

namespace OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory.Contracts
{
    public class ApplicationSettingCategorySearchDto : PagedRequestDto
    {
        public string Name { get; set; }
    }
}
