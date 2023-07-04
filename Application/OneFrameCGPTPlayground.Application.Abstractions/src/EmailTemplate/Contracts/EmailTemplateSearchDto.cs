// <copyright file="EmailTemplateSearchDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;

namespace OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts
{
    public class EmailTemplateSearchDto : PagedRequestDto
    {
        public string Name { get; set; }
    }
}