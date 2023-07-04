// <copyright file="ApiClaimResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.ClaimHelper
{
    public class ApiClaimResponse
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}