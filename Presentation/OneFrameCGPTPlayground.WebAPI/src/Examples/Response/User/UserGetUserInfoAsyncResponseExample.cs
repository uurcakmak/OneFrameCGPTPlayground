// <copyright file="UserGetUserInfoAsyncResponseExample.cs" company="KocSistem">
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
    internal class UserGetUserInfoAsyncResponseExample : IExamplesProvider<ServiceResponse<BasicUserInfoResponse>>
    {
        public ServiceResponse<BasicUserInfoResponse> GetExamples()
        {
            return new ServiceResponse<BasicUserInfoResponse>(new BasicUserInfoResponse
            {
                Name = "Ghost",
                Surname = "Busters",
                Email = "ghostbusters@test.com",
                EmailConfirmed = true,
                PhoneNumber = "5058849801",
            });
        }
    }
}