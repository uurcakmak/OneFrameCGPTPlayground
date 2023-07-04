// <copyright file="ClaimManager.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Caching;
using KocSistem.OneFrame.Common.Cache.Configs;
using KocSistem.OneFrame.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace OneFrameCGPTPlayground.Common.Helpers
{
    /// <summary>
    /// ClaimManager.
    /// </summary>
    /// <seealso cref="IClaimManager" />
    public class ClaimManager : IClaimManager
    {
        private readonly IKsDistributedCache _cache;
        private readonly ICacheConfig _cacheConfig;
        private readonly IHttpContextAccessor _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimManager"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="cacheConfig">The cache configuration.</param>
        /// <param name="cache">The cache.</param>
        public ClaimManager(IHttpContextAccessor context, ICacheConfig cacheConfig, IKsDistributedCache cache)
        {
            _context = context;
            _cacheConfig = cacheConfig;
            _cache = cache;
        }

        /// <summary>
        /// Gets the claims.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaims()
        {
            IEnumerable<Claim> claims;
            if (_cacheConfig.Enabled)
            {
                var claimString = _cache.GetStringAsync(_context.HttpContext.User.Identity.GetClaimCacheKey()).Result ?? "[]";

                claims = JsonConvert.DeserializeObject<List<Claim>>(claimString, new ClaimConverter()) ?? new List<Claim>();
            }
            else
            {
                claims = _context.HttpContext.User.Claims;
            }

            return claims;
        }

        /// <summary>
        /// Sets the claims.
        /// </summary>
        /// <param name="infoClaims">The information claims.</param>
        /// <param name="roleClaims">The role claims.</param>
        /// <param name="userId">The user identifier.</param>
        public void SetClaims(List<Claim> infoClaims, List<Claim> roleClaims, string userId)
        {
            if (_cacheConfig.Enabled)
            {
                roleClaims.AddRange(infoClaims);
                var claimsBytes = JsonConvert.SerializeObject(roleClaims).ToByteArray(Encoding.UTF8);

                if (_cacheConfig.Provider == CacheProvider.MemoryCache)
                {
                    var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(_cacheConfig.DefaultSlidingExpiration));
                    _cache.Set(IdentityExtensions.GetClaimCacheKey(userId), claimsBytes, options);
                }
                else
                {
                    _cache.Set(IdentityExtensions.GetClaimCacheKey(userId), claimsBytes);
                }
            }
            else
            {
                infoClaims.AddRange(roleClaims);
            }
        }

        /// <summary>
        /// Sets the claims.
        /// </summary>
        /// <param name="infoClaims">The information claims.</param>
        /// <param name="roleClaims">The role claims.</param>
        public void SetClaims(List<Claim> infoClaims, List<Claim> roleClaims)
        {
            SetClaims(infoClaims, roleClaims, _context.HttpContext.User.Identity.GetUserId());
        }

        /// <summary>
        /// Sets the claims.
        /// </summary>
        /// <param name="roleClaims">The role claims.</param>
        public void SetClaims(List<Claim> roleClaims)
        {
            SetClaims(new List<Claim>(), roleClaims, _context.HttpContext.User.Identity.GetUserId());
        }
    }
}