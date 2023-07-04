// <copyright file="EmailNotificationResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI
{
    public class EmailNotificationResponse
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public bool IsSent { get; set; }

        public DateTime InsertedDate { get; set; }

        public DateTime? SentDate { get; set; }

        public int RetryCount { get; set; }
    }
}