// <copyright file="ConfirmationCodeModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Profile
{
    public class ConfirmationCodeModel
    {
        public string Code { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string PhoneNumber { get; set; }

        public Guid Id { get; set; }

        public bool IsSent { get; set; }
    }
}