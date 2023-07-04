// <copyright file="EventLog.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Audit.EntityFramework;
using System;

namespace OneFrameCGPTPlayground.Domain
{
    [AuditIgnore]
    public class EventLog
    {
        public Guid Id { get; set; }

        public string AuditData { get; set; }

        public DateTime AuditDate { get; set; }

        public string AuditUser { get; set; }
    }
}
