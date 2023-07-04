// <copyright file="UserDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.User.Contracts
{
    public class UserDto : IDto<Guid>, IInsertAuditing, IUpdateAuditing, ISoftDelete
    {
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public Guid Id { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public string ProfilePhoto { get; set; }

        public string InsertedUser { get; set; }

        public DateTime? InsertedDate { get; set; }

        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string TimeZone { get; set; }
    }
}