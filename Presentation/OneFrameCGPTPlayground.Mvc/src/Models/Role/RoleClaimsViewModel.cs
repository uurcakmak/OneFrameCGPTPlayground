// <copyright file="RoleClaimsViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Role
{
    public class RoleClaimsViewModel
    {
        [Display(Name = "RoleList")]
        public List<SelectListItem> RoleList { get; set; }
    }
}