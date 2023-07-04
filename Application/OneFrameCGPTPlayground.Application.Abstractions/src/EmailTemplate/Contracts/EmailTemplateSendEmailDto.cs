// <copyright file="EmailTemplateSendEmailDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts
{
    public class EmailTemplateSendEmailDto
    {
        public Guid Id { get; set; }

        public int LanguageIndex { get; set; }
    }
}
