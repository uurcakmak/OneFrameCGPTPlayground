// <copyright file="EmailNotificationRequestDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Application.Abstractions
{
    /// <summary>
    /// EmailNotificationRequestDto.
    /// </summary>
    public class EmailNotificationRequestDto
    {
        public string Subject { get; set; }

        public string Content { get; set; }

        public string To { get; set; }

        public string Bcc { get; set; }

        public string Cc { get; set; }
    }
}
