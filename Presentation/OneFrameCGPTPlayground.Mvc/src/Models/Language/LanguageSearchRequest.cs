// <copyright file="LanguageSearchRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Mvc.Models.Paging;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.Language
{
    public class LanguageSearchRequest : PagedRequest
    {
        [Display(Name = "Key")]
        public string Key { get; set; }
    }
}
