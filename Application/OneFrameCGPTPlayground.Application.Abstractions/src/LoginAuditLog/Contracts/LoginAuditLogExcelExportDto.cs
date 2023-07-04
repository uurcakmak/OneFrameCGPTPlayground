// <copyright file="LoginAuditLogExcelExportDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts
{
    public class LoginAuditLogExcelExportDto : IDto<Guid>, IInsertAuditing
    {
        public string ApplicationUserName { get; set; }

        public string BrowserDetail { get; set; }

        public string BrowserGuid { get; set; }

        public string Hostname { get; set; }

        public Guid Id { get; set; }

        public DateTime? InsertedDate { get; set; }

        public string InsertedUser { get; set; }

        public string Ip { get; set; }

        public string MacAddress { get; set; }

        public string Message { get; set; }

        public string OsName { get; set; }

        public string RequestHeaderInfo { get; set; }

        public bool Success { get; set; }

        public string RequestUserName { get; set; }
    }
}