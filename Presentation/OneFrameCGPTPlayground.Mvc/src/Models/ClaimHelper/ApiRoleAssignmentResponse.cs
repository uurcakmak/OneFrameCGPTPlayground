// <copyright file="ApiRoleAssignmentResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.ClaimHelper
{
    public class ApiRoleAssignmentResponse
    {
        public bool IsAssigned { get; set; }

        public string RoleName { get; set; }
    }
}
