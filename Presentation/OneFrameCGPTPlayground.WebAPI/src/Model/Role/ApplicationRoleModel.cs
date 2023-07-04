// <copyright file="ApplicationRoleModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Identity;

namespace OneFrameCGPTPlayground.WebAPI.Model.Role
{
    /// <summary>
    /// Application Role Model.
    /// </summary>
    public class ApplicationRoleModel : IdentityRole<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRoleModel"/> class.
        /// </summary>
        public ApplicationRoleModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRoleModel"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ApplicationRoleModel(string name)
            : base(name)
        {
        }

        public string Description { get; set; }

        public string DisplayText { get; set; }
    }
}