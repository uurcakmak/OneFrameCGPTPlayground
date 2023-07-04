// <copyright file="TwoFactorVerificationRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
using OneFrameCGPTPlayground.Common.Enums;

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class TwoFactorVerificationRequest
    {
        public Login2Fa VerificationType { get; set; }

        public string UserName { get; set; }

        public string VerificationCode { get; set; }
    }
}