// <copyright file="UserRoleUpdateDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class UserRoleUpdateDto
    {
        public List<string> AssignedRoles { get; set; }

        public List<string> UnassignedRoles { get; set; }
    }
}