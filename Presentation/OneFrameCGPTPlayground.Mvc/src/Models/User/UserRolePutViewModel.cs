// <copyright file="UserRolePutViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.User
{
    public class UserRolePutViewModel
    {
        public List<string> AssignedRoles { get; set; }

        public List<string> UnassignedRoles { get; set; }

        public List<SelectListItem> Roles { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
