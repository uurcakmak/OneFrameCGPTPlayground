// <copyright file="ApplicationUser.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Domain
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser<Guid>, IEntity, IInsertAuditing, IUpdateAuditing, ISoftDelete
    {
        public DateTime? InsertedDate { get; set; }

        public string InsertedUser { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public string Name { get; set; }

        public string ProfilePhoto { get; set; }

        public string Surname { get; set; }

        public string TimeZone { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedUser { get; set; }
    }
}