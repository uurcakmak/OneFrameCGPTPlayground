﻿// <copyright file="ResetPasswordDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}