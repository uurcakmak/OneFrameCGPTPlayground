// <copyright file="AccountGetWithUsernameResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountGetWithUsernameResponseExample : IExamplesProvider<ServiceResponse<UserGetResponse>>
    {
        public ServiceResponse<UserGetResponse> GetExamples()
        {
            return new ServiceResponse<UserGetResponse>(new UserGetResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Ghost",
                Surname = "Busters",
                Email = "ghostbusters@kocsistem.com.tr",
                Username = "ghostbusters@kocsistem.com.tr",
                PhoneNumber = "01234567890",
                IsActive = true,
                IsLocked = false,
            });
        }
    }
}