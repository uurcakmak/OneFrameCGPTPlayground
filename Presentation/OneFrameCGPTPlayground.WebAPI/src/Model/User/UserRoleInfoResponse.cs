// <copyright file="UserRoleInfoResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class UserRoleInfoResponse
    {
        public string Email { get; set; }

        public string Id { get; set; }

        public bool IsInRole { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }
    }
}
