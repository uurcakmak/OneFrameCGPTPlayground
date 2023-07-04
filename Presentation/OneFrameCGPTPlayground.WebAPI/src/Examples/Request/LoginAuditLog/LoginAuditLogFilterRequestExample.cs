// <copyright file="LoginAuditLogFilterRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.LoginAuditLog;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class LoginAuditLogFilterRequestExample : IExamplesProvider<LoginAuditLogFilterRequest>
    {
        public LoginAuditLogFilterRequest GetExamples()
        {
            return new LoginAuditLogFilterRequest
            {
                EndDate = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
            };
        }
    }
}