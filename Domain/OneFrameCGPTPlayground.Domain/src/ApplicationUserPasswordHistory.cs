// <copyright file="ApplicationUserPasswordHistory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Domain
{
    public class ApplicationUserPasswordHistory : IInsertAuditing, IEntity<Guid>
    {
        public Guid Id { get; set; }

        public DateTime? InsertedDate { get; set; }

        public string InsertedUser { get; set; }

        public string PasswordHash { get; set; }

        public virtual ApplicationUser User { get; set; }

        public Guid UserId { get; set; }
    }
}
