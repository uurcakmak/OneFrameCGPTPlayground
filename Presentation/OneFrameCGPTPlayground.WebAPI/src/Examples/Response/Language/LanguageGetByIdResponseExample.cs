// <copyright file="LanguageGetByIdResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response.Language
{
    public class LanguageGetByIdResponseExample : IExamplesProvider<LanguageDto>
    {
        public LanguageDto GetExamples()
        {
            return new LanguageDto
            {
                Id = new Guid("5a41be5e-0cb9-4a3e-a1a7-0244b53134cc"),
                Name = "English",
                Code = "en-EN",
                IsDefault = true,
                IsActive = true,
                Image = "data:image/adadasdas",
            };
        }
    }
}
