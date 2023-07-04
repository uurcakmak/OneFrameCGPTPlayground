// <copyright file="ApiRoleGetResponseModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.User
{
    public class ApiRoleGetResponseModel
    {
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "DisplayText")]
        public string DisplayText { get; set; }

        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
