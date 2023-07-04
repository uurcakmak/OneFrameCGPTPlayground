// <copyright file="RolePutViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Role
{
    public class RolePutViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<RoleTranslationsModel> Translations { get; set; }

        public List<string> UsersInRole { get; set; }

        public List<string> UsersNotInRole { get; set; }
    }
}
