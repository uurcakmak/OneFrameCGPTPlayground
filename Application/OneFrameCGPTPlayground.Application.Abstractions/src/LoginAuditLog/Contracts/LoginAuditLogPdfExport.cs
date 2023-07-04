// <copyright file="LoginAuditLogPdfExport.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.ComponentModel;

namespace OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts
{
    public class LoginAuditLogPdfExport
    {
        [Description("IP")]
        public string Ip { get; set; }

        [Description("HostName")]
        public string Hostname { get; set; }

        [Description("MacAddress")]
        public string MacAddress { get; set; }

        [Description("ApplicationUserName")]
        public string ApplicationUserName { get; set; }

        [Description("OSName")]
        public string OsName { get; set; }

        [Description("BrowserDetail")]
        public string BrowserDetail { get; set; }

        [Description("Success")]
        public bool Success { get; set; }

        [Description("CreationDate")]
        public DateTime InsertedDate { get; set; }
    }
}
