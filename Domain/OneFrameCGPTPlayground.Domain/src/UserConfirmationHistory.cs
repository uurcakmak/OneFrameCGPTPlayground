// <copyright file="UserConfirmationHistory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Domain
{
    public class UserConfirmationHistory : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int CodeType { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Code { get; set; }

        public DateTime ExpiredDate { get; set; }

        public bool IsSent { get; set; }

        public DateTime? SentDate { get; set; }

        public bool IsUsed { get; set; }

        public DateTime? UsedDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}