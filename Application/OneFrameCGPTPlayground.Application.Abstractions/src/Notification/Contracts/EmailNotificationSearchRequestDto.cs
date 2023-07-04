// <copyright file="EmailNotificationSearchRequestDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;

namespace OneFrameCGPTPlayground.Application.Abstractions
{
    /// <summary>
    /// EmailNotificationSearchRequestDto.
    /// </summary>
    public class EmailNotificationSearchRequestDto : PagedRequestDto
    {
        public string Value { get; set; }
    }
}
