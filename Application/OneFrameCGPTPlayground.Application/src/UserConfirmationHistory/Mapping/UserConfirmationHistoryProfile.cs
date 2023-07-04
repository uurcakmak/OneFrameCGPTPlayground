// <copyright file="UserConfirmationHistoryProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory.Contract;

namespace OneFrameCGPTPlayground.Application.UserConfirmationHistory.Mapping
{
    public class UserConfirmationHistoryProfile : Profile
    {
        public UserConfirmationHistoryProfile()
        {
            this.CreateMap<Domain.UserConfirmationHistory, UserConfirmationHistoryDto>().ReverseMap();
        }
    }
}