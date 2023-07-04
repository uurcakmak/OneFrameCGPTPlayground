// <copyright file="KsPermissionPolicyHandler.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Common.Authentication
{
    /// <summary>
    /// KsPermissionPolicyHandler.
    /// </summary>
    public class KsPermissionPolicyHandler : AuthorizationHandler<KsPermissionPolicyRequirement>
    {
        private readonly IClaimManager _claimManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="KsPermissionPolicyHandler"/> class.
        /// </summary>
        /// <param name="claimManager">The claim manager.</param>
        public KsPermissionPolicyHandler(IClaimManager claimManager)
        {
            _claimManager = claimManager;
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, KsPermissionPolicyRequirement requirement)
        {
            var claims = _claimManager.GetClaims();

            if (!claims.Any(c => c.Type == ApplicationPolicyType.KsPermission && c.Value == requirement.Permission))
            {
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}