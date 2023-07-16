// <copyright file="IUserPasswordHistoryService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory.Contracts;
using OneFrameCGPTPlayground.Domain;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory
{
    /// <summary>
    /// IRoleService.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IUserPasswordHistoryService : IApplicationCrudServiceAsync<ApplicationUserPasswordHistory, UserPasswordHistoryDto, Guid>, IApplicationService
    {
        public bool PasswordHistoryValidation(ApplicationUser user, string newPassword);
    }
}
