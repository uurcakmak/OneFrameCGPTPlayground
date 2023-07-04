// <copyright file="RoleGetWithTranslatesResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.Role
{
    public class RoleGetWithTranslatesResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<RoleTranslationsModel> Translations { get; set; }
    }
}
