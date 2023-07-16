// <copyright file="UserPasswordHistoryService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory;
using OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory.Contracts;
using OneFrameCGPTPlayground.Domain;
using System;
using System.Linq;

namespace OneFrameCGPTPlayground.Application.UserPasswordHistory
{
    /// <summary>
    /// User Password History Service.
    /// </summary>
    /// <seealso cref="IUserPasswordHistoryService" />
    public class UserPasswordHistoryService : ApplicationCrudServiceAsync<ApplicationUserPasswordHistory, UserPasswordHistoryDto, Guid>, IUserPasswordHistoryService
    {
        private readonly IRepository<ApplicationUserPasswordHistory> _applicationUserPasswordHistoryRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public UserPasswordHistoryService(IRepository<ApplicationUserPasswordHistory> applicationUserPasswordHistoryRepository, IMapper mapper, IDataManager dataManager, IConfiguration configuration, IPasswordHasher<ApplicationUser> passwordHasher)
            : base(applicationUserPasswordHistoryRepository, mapper, dataManager)
        {
            _applicationUserPasswordHistoryRepository = applicationUserPasswordHistoryRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Passwords the history validation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>bool.</returns>
        public bool PasswordHistoryValidation(ApplicationUser user, string newPassword)
        {
            _ = int.TryParse(_configuration["Identity:Policy:Password:HistoryLimit"], out var passwordHistoryLimit);

            var query = _applicationUserPasswordHistoryRepository.GetQueryable();

            query = query.OrderByDescending(o => o.InsertedDate).Take(passwordHistoryLimit);

            var result = query.Where(w => w.UserId == user.Id).ToList();

            return result.All(item => _passwordHasher.VerifyHashedPassword(user, item.PasswordHash, newPassword) != PasswordVerificationResult.Success);
        }
    }
}