﻿// <copyright file="UserGetConfirmationCodeResponseExample.cs" company="KocSistem">
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
    internal class UserGetConfirmationCodeResponseExample : IExamplesProvider<ServiceResponse<ConfirmationCodeResponse>>
    {
        public ServiceResponse<ConfirmationCodeResponse> GetExamples()
        {
            return new ServiceResponse<ConfirmationCodeResponse>(new ConfirmationCodeResponse
            {
                Id = Guid.NewGuid(),
                PhoneNumber = "01234567890",
                Code = "123456",
                ExpiredDate = DateTime.UtcNow.AddMinutes(3)
            });
        }
    }
}