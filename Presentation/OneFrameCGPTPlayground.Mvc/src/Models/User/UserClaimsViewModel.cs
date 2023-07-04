// <copyright file="UserClaimsViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.User
{
    public class UserClaimsViewModel
    {
        [Display(Name = "UserList")]
        public List<SelectListItem> UserList { get; set; }
    }
}