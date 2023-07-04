// <copyright file="SendTryMailRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate
{
    public class SendTryMailRequest
    {
        public Guid Id { get; set; }

        public int LanguageIndex { get; set; }
    }
}
