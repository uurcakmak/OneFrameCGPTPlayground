// <copyright file="IClaimManager.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.Common.Helpers
{
    /// <summary>
    /// IClaimManager.
    /// </summary>
    public interface IClaimManager
    {
        /// <summary>
        /// Gets the claims.
        /// </summary>
        /// <returns>IEnumerable{Claim}.</returns>
        IEnumerable<Claim> GetClaims();

        /// <summary>
        /// Sets the claims.
        /// </summary>
        /// <param name="infoClaims">The information claims.</param>
        /// <param name="roleClaims">The role claims.</param>
        /// <param name="userId">The user identifier.</param>
        void SetClaims(List<Claim> infoClaims, List<Claim> roleClaims, string userId);

        /// <summary>
        /// Sets the claims.
        /// </summary>
        /// <param name="infoClaims">The information claims.</param>
        /// <param name="roleClaims">The role claims.</param>
        void SetClaims(List<Claim> infoClaims, List<Claim> roleClaims);

        /// <summary>
        /// Sets the claims.
        /// </summary>
        /// <param name="roleClaims">The role claims.</param>
        void SetClaims(List<Claim> roleClaims);
    }
}
