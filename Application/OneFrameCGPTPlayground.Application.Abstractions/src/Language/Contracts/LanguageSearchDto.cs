// <copyright file="LanguageSearchDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;

namespace OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts
{
    public class LanguageSearchDto : PagedRequestDto
    {
        public string Key { get; set; }
    }
}
