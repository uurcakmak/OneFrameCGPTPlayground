﻿// <copyright file="UserInRoleResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class UserInRoleResponse
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public string Username { get; set; }
    }
}