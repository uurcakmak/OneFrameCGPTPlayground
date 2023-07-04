// <copyright file="RoleUpdateDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation.Contracts;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts
{
    public class RoleUpdateDto
    {
        public string RoleName { get; set; }

        public List<ApplicationRoleTranslationDto> Translations { get; set; }

        public List<string> UsersInRole { get; set; }

        public List<string> UsersNotInRole { get; set; }
    }
}
