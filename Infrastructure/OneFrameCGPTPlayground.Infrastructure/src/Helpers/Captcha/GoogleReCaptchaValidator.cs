// <copyright file="GoogleReCaptchaValidator.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Infrastructure.Helpers.Captcha
{
    public class GoogleReCaptchaValidator : ICaptchaValidator
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _remoteAddress;
        private string _secretKey;

        public GoogleReCaptchaValidator(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = _httpContextAccessor.HttpContext.RequestServices.GetService<IHttpClientFactory>();
            _secretKey = configuration["ReCaptcha:SecretKey"];
            _remoteAddress = configuration["ReCaptcha:RemoteAddress"];
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "error")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2234:Pass system uri objects instead of strings", Justification = "error")]
        public async Task<bool> IsCaptchaPassedAsync(string token)
        {
            var httpClient = _httpClientFactory.CreateClient("captcha");

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", token),
            });

            var response = await httpClient.PostAsync(_remoteAddress, content).ConfigureAwait(false);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jsonObject = JObject.Parse(jsonResult);

            if (!bool.TryParse(jsonObject["success"].ToString(), out var result))
            {
                result = false;
            }

            return result;
        }

        public void UpdateSecretKey(string key)
        {
            _secretKey = key;
        }
    }
}