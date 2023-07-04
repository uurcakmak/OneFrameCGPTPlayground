// <copyright file="HealthCheckResponseWriter.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Common.Helpers
{
    /// <summary>
    /// ResponseWriter.
    /// </summary>
    public static class HealthCheckResponseWriter
    {
        /// <summary>
        /// Healthes the check response.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static Task HealthCheckResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString()),
                new JProperty("totalDuration", result.TotalDuration.ToString()),
                new JProperty("entries", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("data", new JObject(pair.Value.Data.Select(p => new JProperty(p.Key, p.Value)))),
                        new JProperty("duration", pair.Value.Duration),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("exception", new JObject(
                            new JProperty("message", pair.Value.Exception?.Message.ToString() ?? string.Empty),
                            new JProperty("innerException", pair.Value.Exception?.InnerException?.ToString() ?? string.Empty),
                            new JProperty("stackTrace", pair.Value.Exception?.StackTrace?.ToString() ?? string.Empty))),
                        new JProperty("status", pair.Value.Status.ToString()),
                        new JProperty("tags", pair.Value.Tags)))))));

            return context.Response.WriteAsync(json.ToString(Formatting.Indented));
        }
    }
}