// <copyright file="LanguagePutRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.Language
{
    public class LanguagePutRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Image { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }
    }
}
