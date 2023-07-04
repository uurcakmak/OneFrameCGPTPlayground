// <copyright file="RolePostRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.Role
{
    public class RolePostRequest
    {
        [Required]
        public string Name { get; set; }

        public List<RoleTranslationsModel> Translations { get; set; }
    }
}
