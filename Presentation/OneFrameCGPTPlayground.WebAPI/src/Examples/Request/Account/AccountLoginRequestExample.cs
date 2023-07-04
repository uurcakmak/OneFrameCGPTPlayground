﻿// <copyright file="AccountLoginRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountLoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            var password = Guid.NewGuid().ToString().Remove(5);

            return new LoginRequest
            {
                Email = "ghostbusters@kocsistem.com.tr",
                Password = password,
            };
        }
    }
}