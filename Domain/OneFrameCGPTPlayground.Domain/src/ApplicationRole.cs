// <copyright file="ApplicationRole.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Identity;
using OneFrameCGPTPlayground.Domain.TranslationBase;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Domain
{
    /// <summary>
    /// Application Role.
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>, IMainTableTranslation<ApplicationRoleTranslation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        public ApplicationRole()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRole"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ApplicationRole(string name)
            : base(name)
        {
        }

        public List<ApplicationRoleTranslation> Translations { get; set; }
    }
}