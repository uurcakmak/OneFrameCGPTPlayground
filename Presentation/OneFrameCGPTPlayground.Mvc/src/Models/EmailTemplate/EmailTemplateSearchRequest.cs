// <copyright file="EmailTemplateSearchRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Mvc.Models.Paging;

namespace OneFrameCGPTPlayground.Mvc.Models.EmailTemplate
{
    public class EmailTemplateSearchRequest : PagedRequest
    {
        public string Name { get; set; }
    }
}