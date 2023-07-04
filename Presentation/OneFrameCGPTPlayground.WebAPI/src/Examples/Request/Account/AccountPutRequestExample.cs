// <copyright file="AccountPutRequestExample.cs" company="KocSistem">
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
    internal class AccountPutRequestExample : IExamplesProvider<UserPutRequest>
    {
        public UserPutRequest GetExamples()
        {
            return new UserPutRequest
            {
                Name = "Ghost",
                Surname = "Busters",
                Email = "ghostbusters@kocsistem.com.tr",
                PhoneNumber = "*1234567890",
                IsActive = true,
                IsLocked = false,
            };
        }
    }
}