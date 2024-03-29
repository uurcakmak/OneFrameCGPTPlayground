﻿// <copyright file="UserSearchDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    /// <summary>
    /// User Search Dto.
    /// </summary>
    /// <seealso cref="PagedRequestDto" />
    public class UserSearchDto : PagedRequestDto
    {
        public string Username { get; set; }
    }
}
