// <copyright file="AccountSearchResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class AccountSearchResponseExample : IExamplesProvider<ServiceResponse<PagedResult<UserGetResponse>>>
    {
        public ServiceResponse<PagedResult<UserGetResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<UserGetResponse>>(new PagedResult<UserGetResponse>
            {
                PageSize = 10,
                PageIndex = 0,
                TotalCount = 100,
                TotalPages = 10,
                Items = new List<UserGetResponse>
                {
                    new UserGetResponse
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Ghost",
                        Surname = "User",
                        Email = "ghostbusters@kocsistem.com.tr",
                        Username = "ghostbusters@kocsistem.com.tr",
                        PhoneNumber = "01234567890",
                        IsActive = true,
                        IsLocked = false,
                    },
                },
            });
        }
    }
}