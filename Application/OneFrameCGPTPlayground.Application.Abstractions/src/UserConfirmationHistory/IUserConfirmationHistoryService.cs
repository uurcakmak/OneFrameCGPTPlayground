// <copyright file="IUserConfirmationHistoryService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory.Contract;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using System;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory
{
    public interface IUserConfirmationHistoryService : IApplicationCrudServiceAsync<Domain.UserConfirmationHistory, UserConfirmationHistoryDto, Guid>, IApplicationService
    {
        /// <summary>
        /// Creates confirmation code async.
        /// </summary>
        /// <param name="confirmationHistoryDto">The confiramtion code object is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        Task<ServiceResponse<UserConfirmationHistoryDto>> CreateCodeAsync(UserConfirmationHistoryDto confirmationHistoryDto);

        /// <summary>
        /// Checks confirmation code as sent.
        /// </summary>
        /// <param name="id">The confirmation code id is type of guid.</param>
        /// <returns></returns>
        Task<ServiceResponse> CheckCodeAsSentAsync(Guid id);

        /// <summary>
        /// Confirms phone number confirmation code.
        /// </summary>
        /// <param name="confirmationHistoryDto">The confiramtion code object is type of UserConfirmationHistoryDto.</param>
        /// <returns></returns>
        Task<ServiceResponse> ConfirmCodeAsync(UserConfirmationHistoryDto confirmationHistoryDto);

        /// <summary>
        /// Gets a specific user's active confirmation code.
        /// </summary>
        /// <param name="userId">The user id is type of guid.</param>
        /// <param name="phoneNumber">The phone number is type of string.</param>
        /// <returns></returns>
        Task<ServiceResponse<UserConfirmationHistoryDto>> GetActiveCodeAsync(Guid userId, string phoneNumber);
    }
}