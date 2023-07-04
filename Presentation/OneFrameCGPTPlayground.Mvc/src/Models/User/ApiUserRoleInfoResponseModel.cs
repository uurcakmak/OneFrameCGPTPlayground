// <copyright file="ApiUserRoleInfoResponseModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.User
{
    public class ApiUserRoleInfoResponseModel
    {
        [Display(Name = "EmailAddress")]
        public string Email { get; set; }

        public string Id { get; set; }

        public bool IsInRole { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
