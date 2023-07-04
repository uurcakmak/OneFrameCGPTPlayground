// <copyright file="RoleViewModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Role
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Translations = new List<RoleTranslationsModel>();
        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<RoleTranslationsModel> Translations { get; set; }
    }
}
