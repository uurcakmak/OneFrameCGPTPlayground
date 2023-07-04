// <copyright file="ClientProxy.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Authentication.Utilities;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Common.Proxy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Infrastructure.Helpers.Client
{
    /// <summary>
    /// ProxyHelper.
    /// </summary>
    /// <seealso cref="IClientProxy" />
    public class ClientProxy : IClientProxy
    {
        private readonly string _authenticationScheme = JwtBearerDefaults.AuthenticationScheme;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProxyHelper _proxyHelper;
        private readonly string _refreshPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientProxy"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="proxyHelper">The proxyHelper.</param>
        public ClientProxy(IHttpContextAccessor httpContextAccessor, IConfiguration config, IProxyHelper proxyHelper)
        {
            _ = httpContextAccessor.ThrowIfNull(nameof(httpContextAccessor));
            _ = config.ThrowIfNull(nameof(config));

            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor.HttpContext != null)
            {
                _httpClientFactory = _httpContextAccessor.HttpContext.RequestServices.GetService<IHttpClientFactory>();
            }

            _refreshPath = config["Identity:Jwt:IssuerSettings:RenewToken"];
            _proxyHelper = proxyHelper;
        }

        /// <summary>
        /// Accesses the token refresh wrapper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initialRequest">The initial request.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <param name="addCookies">if set to <c>true</c> [add addCookies].</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<T> AccessTokenRefreshWrapper<T>(Func<Task<HttpResponseMessage>> initialRequest, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false)
        {
            var requestHeaders = new HeaderDictionary();

            if (addAllHeaders)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    foreach (var requestHeader in _httpContextAccessor.HttpContext.Request.Headers)
                    {
                        _ = requestHeaders.TryAdd(requestHeader.Key, requestHeader.Value);
                    }
                }
            }
            else
            {
                _ = requestHeaders.TryAdd("Accept-Language", CultureInfo.CurrentCulture.ToString());

                if (addAuthorization && _httpContextAccessor.HttpContext != null)
                {
                    var accessToken = _httpContextAccessor.HttpContext.User.FindFirst(JwtGrantType.AccessToken)?.Value;
                    _ = requestHeaders.TryAdd("Authorization", new StringValues($"{_authenticationScheme} {accessToken}"));
                }

                if (addCookies)
                {
                    _ = requestHeaders.TryAdd("Cookie", string.Join("; ", _httpContextAccessor.HttpContext.Request.Cookies.Where(x => !x.Key.StartsWith('.')).Select(s => $"{s.Key}={s.Value}")));
                }
            }

            _proxyHelper.SetProxySettings(GetJwtIssuerHttpClient(), requestHeaders);

            _ = initialRequest.ThrowIfNull(nameof(initialRequest));

            var response = await initialRequest().ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken(_refreshPath).ConfigureAwait(false);
                response = await initialRequest().ConfigureAwait(false);
            }

            return await response.Content.ReadAsAsync<T>().ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<T> DeleteApiRequest<T>(string endpoint, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false)
        {
            return await AccessTokenRefreshWrapper<T>(() => _proxyHelper.DeleteApiRequest(endpoint), addAuthorization, addCookies, addAllHeaders).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<T> GetApiRequest<T>(string endpoint, object queryParams = null, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false)
        {
            return await AccessTokenRefreshWrapper<T>(() => _proxyHelper.GetApiRequest(endpoint, queryParams), addAuthorization, addCookies, addAllHeaders).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the JWT issuer HTTP client.
        /// </summary>
        /// <returns></returns>
        public HttpClient GetJwtIssuerHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient("jwtIssuerClient");
            return httpClient;
        }

        /// <summary>
        /// Posts the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<T> PostApiRequest<T>(string endpoint, object model, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false)
        {
            return await AccessTokenRefreshWrapper<T>(() => _proxyHelper.PostApiRequest(endpoint, model), addAuthorization, addCookies, addAllHeaders).ConfigureAwait(false);
        }

        /// <summary>
        /// Puts the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<T> PutApiRequest<T>(string endpoint, object model, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false)
        {
            return await AccessTokenRefreshWrapper<T>(() => _proxyHelper.PutApiRequest(endpoint, model), addAuthorization, addCookies, addAllHeaders).ConfigureAwait(false);
        }

        /// <summary>
        /// Refreshes the access token.
        /// </summary>
        /// <param name="endPoint">The end point.</param>
        private async Task RefreshAccessToken(string endPoint)
        {
            var refreshToken = _httpContextAccessor.HttpContext.User.FindFirst(JwtGrantType.RefreshToken)?.Value;
            var token = _httpContextAccessor.HttpContext.User.FindFirst(JwtGrantType.AccessToken)?.Value;

            var model = new { Token = token, RefreshToken = refreshToken };

            var response = await PostApiRequest<AccessToken>(endPoint, model, false).ConfigureAwait(false);

            var tokenModel = response;

            var authInfo = await _httpContextAccessor.HttpContext.AuthenticateAsync(_authenticationScheme).ConfigureAwait(false);

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadToken(tokenModel.Token) as JwtSecurityToken;

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,
                ExpiresUtc = DateTime.UnixEpoch.AddSeconds(jwtSecurityToken.Payload.Exp.Value),
                IsPersistent = true,
            };

            var identity = authInfo.Principal.Identity as ClaimsIdentity;
            var jwtClaim = identity.FindFirst(JwtGrantType.AccessToken);
            var refreshTokenClaim = identity.FindFirst(JwtGrantType.RefreshToken);

            if (jwtClaim != null)
            {
                identity.RemoveClaim(jwtClaim);
            }

            if (refreshTokenClaim != null)
            {
                identity.RemoveClaim(refreshTokenClaim);
            }

            identity.AddClaim(new Claim(JwtGrantType.AccessToken, tokenModel.Token));
            identity.AddClaim(new Claim(JwtGrantType.RefreshToken, tokenModel.RefreshToken));

            await _httpContextAccessor.HttpContext.SignInAsync(_authenticationScheme, authInfo.Principal, authProperties).ConfigureAwait(false);
        }
    }
}