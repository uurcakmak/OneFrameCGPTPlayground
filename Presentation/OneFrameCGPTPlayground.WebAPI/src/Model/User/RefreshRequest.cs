// <copyright file="RefreshRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }

        public string Token { get; set; }
    }
}