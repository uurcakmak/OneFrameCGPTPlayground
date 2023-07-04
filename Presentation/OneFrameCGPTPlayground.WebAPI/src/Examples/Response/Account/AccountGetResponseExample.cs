// <copyright file="AccountGetResponseExample.cs" company="KocSistem">
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
    internal class AccountGetResponseExample : IExamplesProvider<ServiceResponse<PagedResult<UserGetResponse>>>
    {
        public ServiceResponse<PagedResult<UserGetResponse>> GetExamples()
        {
            return new ServiceResponse<PagedResult<UserGetResponse>>(new PagedResult<UserGetResponse>
            {
                Items = new List<UserGetResponse>
                {
                    new UserGetResponse
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Ghost",
                        Surname = "Busters",
                        Email = "ghostbusters@kocsistem.com.tr",
                        Username = "ghostbusters@kocsistem.com.tr",
                        PhoneNumber = "01234567890",
                        IsActive = true,
                        IsLocked = false,
                    },
                },
                PageIndex = 0,
                PageSize = 10,
                TotalCount = 100,
                TotalPages = 10,
            });
        }
    }
}