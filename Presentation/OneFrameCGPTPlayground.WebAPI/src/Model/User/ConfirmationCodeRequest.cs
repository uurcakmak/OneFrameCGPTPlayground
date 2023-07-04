// <copyright file="ConfirmationCodeRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class ConfirmationCodeRequest
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string PhoneNumber { get; set; }
    }
}