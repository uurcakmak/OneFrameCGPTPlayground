// <copyright file="LoginAuditLogFilterDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts
{
    public class LoginAuditLogFilterDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
