// <copyright file="SaveUserClaimsModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class SaveUserClaimsModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "StringLengthValidationError", MinimumLength = 2)]
        public string Name { get; set; }

        public List<string> SelectedUserClaimList { get; set; }
    }
}