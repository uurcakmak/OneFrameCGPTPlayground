// <copyright file="UserRolePutRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class UserRolePutRequest
    {
        public List<string> AssignedRoles { get; set; }

        public List<string> UnassignedRoles { get; set; }

        public string Username { get; set; }
    }
}