// <copyright file="UserPasswordHistoryDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory.Contracts
{
    /// <summary>
    /// User Password History Dto.
    /// </summary>
    /// <seealso cref="Guid" />
    public class UserPasswordHistoryDto : IDto<Guid>, IInsertAuditing
    {
        public Guid Id { get; set; }

        public DateTime? InsertedDate { get; set; }

        public string InsertedUser { get; set; }

        public string PasswordHash { get; set; }

        public Guid UserId { get; set; }
    }
}
