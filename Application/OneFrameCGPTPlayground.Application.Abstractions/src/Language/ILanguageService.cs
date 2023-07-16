// <copyright file="ILanguageService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts;
using System;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.Language
{
    public interface ILanguageService : IApplicationCrudServiceAsync<Domain.Language, LanguageDto, Guid>, IApplicationService
    {
        Task<ServiceResponse<PagedResultDto<LanguageDto>>> GetLanguageListAsync(PagedRequestDto pagedRequest);

        Task<ServiceResponse<PagedResultDto<LanguageDto>>> SearchAsync(LanguageSearchDto languageGetRequest);
    }
}
