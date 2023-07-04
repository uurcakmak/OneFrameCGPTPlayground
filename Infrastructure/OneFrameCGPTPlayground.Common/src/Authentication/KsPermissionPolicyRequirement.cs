// <copyright file="KsPermissionPolicyRequirement.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Authorization;

namespace OneFrameCGPTPlayground.Common.Authentication
{
    /// <summary>
    /// KsPermissionPolicyRequirement.
    /// </summary>
    /// <seealso cref="IAuthorizationRequirement" />
    public class KsPermissionPolicyRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KsPermissionPolicyRequirement"/> class.
        /// </summary>
        /// <param name="permission">The permission.</param>
        public KsPermissionPolicyRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
}
