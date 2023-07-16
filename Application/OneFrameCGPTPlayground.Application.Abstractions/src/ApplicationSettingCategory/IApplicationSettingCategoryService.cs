// <copyright file="IApplicationSettingCategoryService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using System;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory
{
    public interface IApplicationSettingCategoryService : IApplicationCrudServiceAsync<Domain.ApplicationSettingCategory, ApplicationSettingCategoryDto, Guid>, IApplicationService
    {
        Task<ServiceResponse<PagedResultDto<ApplicationSettingCategoryDto>>> GetApplicationSettingCategoryListAsync(PagedRequestDto pagedRequest);

        Task<ServiceResponse<ApplicationSettingCategoryDto>> CreateApplicationSettingCategoryAsync(ApplicationSettingCategoryDto applicationSettingCategoryDto);

        Task<ServiceResponse<ApplicationSettingCategoryDto>> UpdateApplicationSettingCategoryAsync(ApplicationSettingCategoryDto applicationSettingCategoryDto);

        Task<ServiceResponse> DeleteApplicationSettingCategoryAsync(Guid applicationSettingCategoryId);

        Task<ServiceResponse<ApplicationSettingCategoryDto>> GetApplicationSettingCategoryByIdAsync(Guid id);

        Task<ServiceResponse<PagedResultDto<ApplicationSettingCategoryDto>>> SearchAsync(ApplicationSettingCategorySearchDto applicationSettingCategoryGetRequest);
    }
}
