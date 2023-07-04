// <copyright file="EmailTemplateModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate
{
    public class EmailTemplateModel
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string EmailContent { get; set; }
    }
}
