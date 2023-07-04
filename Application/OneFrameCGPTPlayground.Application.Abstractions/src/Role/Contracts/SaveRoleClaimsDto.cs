// <copyright file="SaveRoleClaimsDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts
{
    public class SaveRoleClaimsDto
    {
        public string Name { get; set; }

        public List<string> SelectedRoleClaimList { get; set; }
    }
}