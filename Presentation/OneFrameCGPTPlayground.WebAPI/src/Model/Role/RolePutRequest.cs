// <copyright file="RolePutRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.Role
{
    public class RolePutRequest
    {
        public List<RoleTranslationsModel> Translations { get; set; }

        public List<string> UsersInRole { get; set; }

        public List<string> UsersNotInRole { get; set; }
    }
}
