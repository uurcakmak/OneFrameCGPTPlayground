// <copyright file="IClientProxy.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Infrastructure.Helpers.Client
{
    /// <summary>
    /// IProxyHelper.
    /// </summary>
    public interface IClientProxy
    {
        /// <summary>
        /// Accesses the token refresh wrapper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initialRequest">The initial request.</param>
        /// <param name="addAuthorization">addAuthorization.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> AccessTokenRefreshWrapper<T>(Func<Task<HttpResponseMessage>> initialRequest, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false);

        /// <summary>
        /// Deletes the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> DeleteApiRequest<T>(string endpoint, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false);

        /// <summary>
        /// Gets the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="queryParams">The query parameters.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> GetApiRequest<T>(string endpoint, object queryParams = null, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false);

        /// <summary>
        /// Gets the JWT issuer HTTP client.
        /// </summary>
        /// <returns></returns>
        HttpClient GetJwtIssuerHttpClient();

        /// <summary>
        /// Posts the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> PostApiRequest<T>(string endpoint, object model, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false);

        /// <summary>
        /// Puts the API request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="model">The model.</param>
        /// <param name="addAuthorization">if set to <c>true</c> [add authorization].</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<T> PutApiRequest<T>(string endpoint, object model, bool addAuthorization = true, bool addCookies = false, bool addAllHeaders = false);
    }
}