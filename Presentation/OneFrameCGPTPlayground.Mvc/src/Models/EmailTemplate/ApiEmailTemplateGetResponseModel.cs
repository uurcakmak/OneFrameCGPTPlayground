// <copyright file="ApiEmailTemplateGetResponseModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.EmailTemplate
{
    public class ApiEmailTemplateGetResponseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string SupportedLanguages { get; set; }
    }
}
