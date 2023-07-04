// <copyright file="SaveUserClaimsDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class SaveUserClaimsDto
    {
        [Required]
        public string Name { get; set; }

        public List<string> SelectedUserClaimList { get; set; }
    }
}