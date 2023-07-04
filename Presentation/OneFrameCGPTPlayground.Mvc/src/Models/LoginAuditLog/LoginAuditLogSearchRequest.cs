// <copyright file="LoginAuditLogSearchRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Mvc.Models.Paging;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.LoginAuditLog
{
    public class LoginAuditLogSearchRequest : PagedRequest
    {
        [Display(Name = "Value")]
        public string Value { get; set; }
    }
}
