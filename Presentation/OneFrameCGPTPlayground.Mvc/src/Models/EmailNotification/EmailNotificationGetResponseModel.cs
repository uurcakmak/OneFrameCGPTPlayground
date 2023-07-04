// <copyright file="EmailNotificationGetResponseModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc
{
    public class EmailNotificationGetResponseModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Display(Name = "Body")]
        public string Body { get; set; }

        [Display(Name = "From")]
        public string From { get; set; }

        [Display(Name = "To")]
        public string To { get; set; }

        [Display(Name = "Cc")]
        public string Cc { get; set; }

        [Display(Name = "Bcc")]
        public string Bcc { get; set; }

        [Display(Name = "IsSent")]
        public bool IsSent { get; set; }

        [Display(Name = "InsertedDate")]
        public DateTime InsertedDate { get; set; }

        [Display(Name = "SentDate")]
        public DateTime SentDate { get; set; }

        [Display(Name = "RetryCount")]
        public int RetryCount { get; set; }
    }
}
