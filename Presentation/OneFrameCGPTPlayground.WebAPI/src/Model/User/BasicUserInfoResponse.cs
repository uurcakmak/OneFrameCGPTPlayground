// <copyright file="BasicUserInfoResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class BasicUserInfoResponse
    {
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }

        public string ProfilePhoto { get; set; }

        public string TimeZone { get; set; }
    }
}