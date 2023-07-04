// <copyright file="RegisterDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class RegisterDto
    {
        public RegisterDto()
        {
        }

        public IList<ClaimDto> Claims { get; set; }

        public bool PasswordExpired { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool IsActive { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }
    }
}