// <copyright file="IApplicationSettingService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting
{
    public interface IApplicationSettingService : IApplicationCrudServiceAsync<Domain.ApplicationSetting, ApplicationSettingDto, Guid>, IApplicationService
    {
        Task<ServiceResponse<ApplicationSettingDto>> GetByKeyAsync(string key);

        Task<ServiceResponse<Dictionary<string, dynamic>>> GetByKeyAsync(IList<string> keyList, IList<string> categoryNameList);

        Task<ServiceResponse<Dictionary<string, dynamic>>> GetByKeyForCurrentApplicationAsync(string key);

        Task<ServiceResponse<Dictionary<string, dynamic>>> GetByKeyForCurrentApplicationAsync(IList<string> keyList);

        Task<ServiceResponse<PagedResultDto<ApplicationSettingDetailDto>>> GetListAsync(PagedRequestDto pagedRequest);

        Task<ServiceResponse<Dictionary<string, dynamic>>> GetListByCategoryAsync(IList<string> categoryNameList);

        Task<ServiceResponse<PagedResultDto<ApplicationSettingDetailDto>>> SearchAsync(ApplicationSettingSearchDto applicationSettingGetRequest);
    }
}