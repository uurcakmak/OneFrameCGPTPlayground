// <copyright file="UserUpdateDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class UserUpdateDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public List<string> AssignedRoles { get; set; }

        public List<string> UnassignedRoles { get; set; }

        public string TimeZone { get; set; }
    }
}