// <copyright file="UserPasswordHistoryProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory.Contracts;

namespace OneFrameCGPTPlayground.Application.UserPasswordHistory.Mappings
{
    /// <summary>
    /// Menu Translation Profile.
    /// </summary>
    /// <seealso cref="Profile" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UserPasswordHistoryProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPasswordHistoryProfile"/> class.
        /// </summary>
        public UserPasswordHistoryProfile()
        {
            _ = CreateMap<Domain.ApplicationUserPasswordHistory, UserPasswordHistoryDto>().ReverseMap();
        }
    }
}
