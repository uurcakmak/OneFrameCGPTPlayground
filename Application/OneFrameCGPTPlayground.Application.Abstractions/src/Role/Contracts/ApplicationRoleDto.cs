// <copyright file="ApplicationRoleDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.RoleTranslation.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.TranslationBaseDto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts
{
    /// <summary>
    /// Application Role Dto.
    /// </summary>
    /// <seealso cref="Guid" />
    /// <seealso cref="ApplicationRoleTranslationDto" />
    public class ApplicationRoleDto : IdentityRole<Guid>, IMainTableTranslationDto<ApplicationRoleTranslationDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRoleDto"/> class.
        /// </summary>
        public ApplicationRoleDto()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRoleDto"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ApplicationRoleDto(string name)
            : base(name)
        {
        }

        public List<ApplicationRoleTranslationDto> Translations { get; set; }
    }
}
