// <copyright file="AuthenticationGenerateAuthenticatorSharedKeyResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Authentication;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AuthenticationGenerateAuthenticatorSharedKeyResponseExample : IExamplesProvider<AuthenticatorResponse>
    {
        public AuthenticatorResponse GetExamples()
        {
            return new AuthenticatorResponse
            {
                HasAuthenticatorKey = false,
                IsActivated = false,
                SharedKey = string.Empty,
            };
        }
    }
}