// <copyright file="UserSearchRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Mvc.Models.Paging;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.User
{
    public class UserSearchRequest : PagedRequest
    {
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}