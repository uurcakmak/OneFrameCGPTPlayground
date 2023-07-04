// <copyright file="LoginAuditLogGetResponseModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc
{
    public class LoginAuditLogGetResponseModel
    {
        [Display(Name = "ApplicationUserName")]
        public string ApplicationUserName { get; set; }

        [Display(Name = "BrowserDetail")]
        public string BrowserDetail { get; set; }

        [Display(Name = "BrowserGuid")]
        public string BrowserGuid { get; set; }

        [Display(Name = "Hostname")]
        public string Hostname { get; set; }

        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "InsertedDate")]
        public DateTime? InsertedDate { get; set; }

        [Display(Name = "IP")]
        public string Ip { get; set; }

        [Display(Name = "MacAddress")]
        public string MacAddress { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "OSName")]
        public string OsName { get; set; }

        [Display(Name = "RequestHeaderInfo")]
        public string RequestHeaderInfo { get; set; }

        [Display(Name = "Success")]
        public bool Success { get; set; }

        [Display(Name = "RequestUserName")]
        public string RequestUserName { get; set; }
    }
}
