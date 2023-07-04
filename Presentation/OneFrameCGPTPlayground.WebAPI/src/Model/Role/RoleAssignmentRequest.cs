// <copyright file="RoleAssignmentRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.Role
{
    public class RoleAssignmentRequest
    {
        public string RoleName { get; set; }

        public bool IsAssigned { get; set; }
    }
}