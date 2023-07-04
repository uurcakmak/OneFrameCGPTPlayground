// <copyright file="LoginDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class LoginDto
    {
        public LoginDto()
        {
        }

        public IList<ClaimDto> Claims { get; set; }

        public bool PasswordExpired { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool IsActive { get; set; }
    }
}