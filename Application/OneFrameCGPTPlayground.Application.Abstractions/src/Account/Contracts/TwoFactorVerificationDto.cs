// -----------------------------------------------------------------------
// <copyright file="TwoFactorVerificationDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using OneFrameCGPTPlayground.Common.Enums;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class TwoFactorVerificationDto
    {
        public Login2Fa VerificationType { get; set; }

        public string Username { get; set; }

        public string VerificationCode { get; set; }
    }
}