// <copyright file="ApplicationSettingService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.ApplicationSetting;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Setting
{
    public class ApplicationSettingService : ApplicationCrudServiceAsync<Domain.ApplicationSetting, ApplicationSettingDto, Guid>, IApplicationSettingService
    {
        private readonly IApplicationSettingConfig _applicationSettingConfig;
        private readonly IRepository<Domain.ApplicationSetting> _applicationSettingRepository;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IKsStringLocalizer<ApplicationSettingService> _localize;
        private readonly IMapper _mapper;
        private readonly IServiceResponseHelper _serviceResponseHelper;

        public ApplicationSettingService(IRepository<Domain.ApplicationSetting> applicationSettingRepository, IMapper mapper, IDataManager dataManager, IKsI18N i18N, IServiceResponseHelper serviceResponseHelper, ILookupNormalizer keyNormalizer, IApplicationSettingConfig applicationSettingConfig)
            : base(applicationSettingRepository, mapper, dataManager)
        {
            _applicationSettingRepository = applicationSettingRepository;
            _mapper = mapper;
            _serviceResponseHelper = serviceResponseHelper;
            _localize = i18N.GetLocalizer<ApplicationSettingService>();
            _keyNormalizer = keyNormalizer;
            _applicationSettingConfig = applicationSettingConfig;
        }

        public override async Task<ServiceResponse<ApplicationSettingDto>> CreateAsync(ApplicationSettingDto model)
        {
            if (model == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["ApplicationSettingUpdateModelNotFound"], StatusCodes.Status400BadRequest, true);
            }

            var isExists = await _applicationSettingRepository
                .GetFirstOrDefaultAsync(predicate: q => q.Key == model.Key).ConfigureAwait(false) != null;

            if (isExists)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["ApplicationSettingAlreadyExists"], StatusCodes.Status400BadRequest, true);
            }

            var entity = _mapper.Map<ApplicationSettingDto, Domain.ApplicationSetting>(model);
            await _applicationSettingRepository.AddAsync(entity).ConfigureAwait(false);

            var dto = _mapper.Map<Domain.ApplicationSetting, ApplicationSettingDto>(entity);

            return _serviceResponseHelper.SetSuccess(dto);
        }

        public override async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var applicationSettingToDelete = await _applicationSettingRepository.GetFirstOrDefaultAsync(a => a, a => a.Id == id).ConfigureAwait(false);

            if (applicationSettingToDelete == null)
            {
                return _serviceResponseHelper.SetError(_localize["ApplicationSettingNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (applicationSettingToDelete.IsStatic)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["YouCannotDeleteTheStaticSetting"], StatusCodes.Status400BadRequest, true);
            }

            await _applicationSettingRepository.DeleteAsync(applicationSettingToDelete).ConfigureAwait(false);
            return _serviceResponseHelper.SetSuccess();
        }

        public override async Task<ServiceResponse<ApplicationSettingDto>> GetByIdAsync(Guid id)
        {
            var applicationSetting = await _applicationSettingRepository.GetFirstOrDefaultAsync(predicate: source => source.Id == id, include: source => source.Include(c => c.Category)).ConfigureAwait(false);

            if (applicationSetting == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["ApplicationSettingNotFound"], StatusCodes.Status204NoContent, true);
            }

            var applicationSettingDto = _mapper.Map<Domain.ApplicationSetting, ApplicationSettingDto>(applicationSetting);
            applicationSettingDto.Value = ConvertTypeOfValue(applicationSetting.ValueType, applicationSetting.Value);

            return _serviceResponseHelper.SetSuccess(applicationSettingDto);
        }

        public async Task<ServiceResponse<ApplicationSettingDto>> GetByKeyAsync(string key)
        {
            var applicationSetting = await _applicationSettingRepository.GetFirstOrDefaultAsync(predicate: source => source.Key == key, include: source => source.Include(c => c.Category)).ConfigureAwait(false);

            if (applicationSetting == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["ApplicationSettingNotFound"], StatusCodes.Status204NoContent, true);
            }

            var applicationSettingDto = _mapper.Map<Domain.ApplicationSetting, ApplicationSettingDto>(applicationSetting);
            applicationSettingDto.Value = ConvertTypeOfValue(applicationSetting.ValueType, applicationSetting.Value);

            return _serviceResponseHelper.SetSuccess(applicationSettingDto);
        }

        public async Task<ServiceResponse<Dictionary<string, dynamic>>> GetByKeyAsync(IList<string> keyList, IList<string> categoryNameList)
        {
            var applicationSettingList = await _applicationSettingRepository.GetListAsync(
                predicate: source => keyList.Any(a => a == source.Key) && categoryNameList.Any(a => a == source.Category.Name), include: source => source.Include(c => c.Category)).ConfigureAwait(false);

            var list = ConvertTypeOfValues(applicationSettingList);

            return _serviceResponseHelper.SetSuccess(list);
        }

        public async Task<ServiceResponse<Dictionary<string, dynamic>>> GetByKeyForCurrentApplicationAsync(string key)
        {
            return await GetByKeyAsync(new List<string> { key }, _applicationSettingConfig.CategoryNameList).ConfigureAwait(false);
        }

        public async Task<ServiceResponse<Dictionary<string, dynamic>>> GetByKeyForCurrentApplicationAsync(IList<string> keyList)
        {
            return await GetByKeyAsync(keyList, _applicationSettingConfig.CategoryNameList).ConfigureAwait(false);
        }

        public async Task<ServiceResponse<PagedResultDto<ApplicationSettingDetailDto>>> GetListAsync(PagedRequestDto pagedRequest)
        {
            var query = _applicationSettingRepository.GetQueryable(include: source => source.Include(c => c.Category));

            if (pagedRequest.Orders != null && pagedRequest.Orders.Any())
            {
                query = pagedRequest.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.Key);
            }

            var applicationSettings = await query.ToPagedListAsync(pagedRequest.PageIndex, pagedRequest.PageSize).ConfigureAwait(false);

            var applicationSettingGetResponse = _mapper.Map<IPagedList<Domain.ApplicationSetting>, PagedResultDto<ApplicationSettingDetailDto>>(applicationSettings);
            return _serviceResponseHelper.SetSuccess(applicationSettingGetResponse);
        }

        public async Task<ServiceResponse<Dictionary<string, dynamic>>> GetListByCategoryAsync(IList<string> categoryNameList)
        {
            var applicationSettingList = await _applicationSettingRepository.GetListAsync(predicate: source => categoryNameList.Any(b => b == source.Category.Name), include: source => source.Include(c => c.Category)).ConfigureAwait(false);

            var list = ConvertTypeOfValues(applicationSettingList);

            return _serviceResponseHelper.SetSuccess(list);
        }

        public async Task<ServiceResponse<PagedResultDto<ApplicationSettingDetailDto>>> SearchAsync(ApplicationSettingSearchDto applicationSettingGetRequest)
        {
            var normalizeName = _keyNormalizer.NormalizeName(applicationSettingGetRequest.Key);

            var applicationSettings = await _applicationSettingRepository.GetQueryable(include: source => source.Include(c => c.Category)).Where(u => u.Key.Contains(normalizeName))
                .ToPagedListAsync(applicationSettingGetRequest.PageIndex, applicationSettingGetRequest.PageSize).ConfigureAwait(false);

            var applicationSettingGetResponse = _mapper.Map<IPagedList<Domain.ApplicationSetting>, PagedResultDto<ApplicationSettingDetailDto>>(applicationSettings);
            return _serviceResponseHelper.SetSuccess(applicationSettingGetResponse);
        }

        public override async Task<ServiceResponse<ApplicationSettingDto>> UpdateAsync(ApplicationSettingDto model)
        {
            if (model == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["ApplicationSettingUpdateModelNotFound"], StatusCodes.Status400BadRequest, true);
            }

            var entity = await _applicationSettingRepository.GetFirstOrDefaultAsync(ap => ap, predicate: ap => ap.Id == model.Id).ConfigureAwait(false);

            if (entity == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["ApplicationSettingNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (entity.Key != model.Key)
            {
                var isExists = await _applicationSettingRepository.GetFirstOrDefaultAsync(predicate: q => q.Key == model.Key).ConfigureAwait(false) != null;

                if (isExists)
                {
                    return _serviceResponseHelper.SetError<ApplicationSettingDto>(null, _localize["ApplicationSettingAlreadyExists"], StatusCodes.Status400BadRequest, true);
                }
            }

            if (!entity.IsStatic)
            {
                entity.Key = model.Key;
            }

            entity.CategoryId = model.CategoryId;
            entity.Value = model.Value;
            entity.ValueType = model.ValueType;
            entity.IsStatic = model.IsStatic;

            await _applicationSettingRepository.UpdateAsync(entity).ConfigureAwait(false);
            var result = _mapper.Map<Domain.ApplicationSetting, ApplicationSettingDto>(entity);

            return _serviceResponseHelper.SetSuccess(result);
        }

        private static dynamic ConvertTypeOfValue(string valueType, string value)
        {
            var type = Type.GetType(valueType) ?? typeof(string);
            var convertedValue = Convert.ChangeType(value, type);
            return convertedValue;
        }

        private static Dictionary<string, dynamic> ConvertTypeOfValues(List<Domain.ApplicationSetting> data)
        {
            var list = new Dictionary<string, dynamic>();

            if (data == null)
            {
                return list;
            }

            foreach (var setting in data)
            {
                var type = Type.GetType(setting.ValueType) ?? typeof(string);

                dynamic value = Convert.ChangeType(setting.Value, type);

                list.Add(setting.Key, value);
            }

            return list;
        }
    }
}