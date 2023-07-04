// <copyright file="RoleAssignmentDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class RoleAssignmentDto
    {
        public string RoleName { get; set; }

        public bool IsAssigned { get; set; }
    }
}
