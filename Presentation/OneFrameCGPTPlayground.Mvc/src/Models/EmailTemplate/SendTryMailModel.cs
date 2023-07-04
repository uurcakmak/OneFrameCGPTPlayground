// <copyright file="SendTryMailModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.EmailTemplate
{
    public class SendTryMailModel
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
