// <copyright file="ApplicationSettingCategoryService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSettingCategory.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Setting
{
    public class ApplicationSettingCategoryService : ApplicationCrudServiceAsync<Domain.ApplicationSettingCategory, ApplicationSettingCategoryDto, Guid>, IApplicationSettingCategoryService
    {
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.ApplicationSettingCategory> _applicationSettingCategoryRepository;
        private readonly IKsStringLocalizer<ApplicationSettingCategoryService> _localize;
        private readonly IServiceResponseHelper _serviceResponseHelper;

        public ApplicationSettingCategoryService(IRepository<Domain.ApplicationSettingCategory> applicationSettingCategoryRepository, IMapper mapper, IDataManager dataManager, IKsI18N i18N, IServiceResponseHelper serviceResponseHelper, ILookupNormalizer keyNormalizer)
            : base(applicationSettingCategoryRepository, mapper, dataManager)
        {
            _applicationSettingCategoryRepository = applicationSettingCategoryRepository;
            _mapper = mapper;
            _serviceResponseHelper = serviceResponseHelper;
            _localize = i18N.GetLocalizer<ApplicationSettingCategoryService>();
            _keyNormalizer = keyNormalizer;
        }

        public async Task<ServiceResponse<ApplicationSettingCategoryDto>> CreateApplicationSettingCategoryAsync(ApplicationSettingCategoryDto applicationSettingCategoryDto)
        {
            var isExists = await _applicationSettingCategoryRepository.GetFirstOrDefaultAsync(predicate: q => q.Name == applicationSettingCategoryDto.Name).ConfigureAwait(false) != null;

            if (isExists)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingCategoryDto>(null, _localize["ApplicationSettingCategoryAlreadyExists"], StatusCodes.Status400BadRequest, true);
            }

            var entity = _mapper.Map<ApplicationSettingCategoryDto, Domain.ApplicationSettingCategory>(applicationSettingCategoryDto);
            await _applicationSettingCategoryRepository.AddAsync(entity).ConfigureAwait(false);

            var dto = _mapper.Map<Domain.ApplicationSettingCategory, ApplicationSettingCategoryDto>(entity);

            return _serviceResponseHelper.SetSuccess(dto);
        }

        public async Task<ServiceResponse<ApplicationSettingCategoryDto>> UpdateApplicationSettingCategoryAsync(ApplicationSettingCategoryDto applicationSettingCategoryDto)
        {
            if (applicationSettingCategoryDto == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingCategoryDto>(null, _localize["ApplicationSettingCategoryUpdateModelNotFound"], StatusCodes.Status400BadRequest, true);
            }

            var entity = await _applicationSettingCategoryRepository.GetFirstOrDefaultAsync(ap => ap, predicate: ap => ap.Id == applicationSettingCategoryDto.Id).ConfigureAwait(false);

            if (entity == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingCategoryDto>(null, _localize["ApplicationSettingCategoryNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (entity.Name != applicationSettingCategoryDto.Name)
            {
                var isExists = await _applicationSettingCategoryRepository.GetFirstOrDefaultAsync(predicate: q => q.Name == applicationSettingCategoryDto.Name).ConfigureAwait(false) != null;

                if (isExists)
                {
                    return _serviceResponseHelper.SetError<ApplicationSettingCategoryDto>(null, _localize["ApplicationSettingCategoryAlreadyExists"], StatusCodes.Status400BadRequest, true);
                }
            }

            entity.Name = applicationSettingCategoryDto.Name;
            entity.Description = applicationSettingCategoryDto.Description;

            await _applicationSettingCategoryRepository.UpdateAsync(entity).ConfigureAwait(false);

            var result = _mapper.Map<Domain.ApplicationSettingCategory, ApplicationSettingCategoryDto>(entity);

            return _serviceResponseHelper.SetSuccess(result);
        }

        public async Task<ServiceResponse> DeleteApplicationSettingCategoryAsync(Guid applicationSettingCategoryId)
        {
            var applicationSettingCategoryToDelete = await _applicationSettingCategoryRepository.GetFirstOrDefaultAsync(predicate: a => a.Id == applicationSettingCategoryId, include: source => source.Include(a => a.ApplicationSettings)).ConfigureAwait(false);

            if (applicationSettingCategoryToDelete == null)
            {
                return _serviceResponseHelper.SetError(_localize["ApplicationSettingCategoryNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (applicationSettingCategoryToDelete.ApplicationSettings?.Count > 0)
            {
                return _serviceResponseHelper.SetError(_localize["ApplicationSettingCategoryContainsActiveSettings"], StatusCodes.Status400BadRequest, true);
            }

            await _applicationSettingCategoryRepository.DeleteAsync(applicationSettingCategoryToDelete).ConfigureAwait(false);

            return _serviceResponseHelper.SetSuccess();
        }

        public async Task<ServiceResponse<ApplicationSettingCategoryDto>> GetApplicationSettingCategoryByIdAsync(Guid id)
        {
            var applicationSetting = await _applicationSettingCategoryRepository.GetFirstOrDefaultAsync(predicate: source => source.Id == id).ConfigureAwait(false);

            if (applicationSetting == null)
            {
                return _serviceResponseHelper.SetError<ApplicationSettingCategoryDto>(null, _localize["ApplicationSettingNotFound"], StatusCodes.Status204NoContent, true);
            }

            var applicationSettingDto = _mapper.Map<Domain.ApplicationSettingCategory, ApplicationSettingCategoryDto>(applicationSetting);
            return _serviceResponseHelper.SetSuccess(applicationSettingDto);
        }

        public async Task<ServiceResponse<PagedResultDto<ApplicationSettingCategoryDto>>> SearchAsync(ApplicationSettingCategorySearchDto applicationSettingCategoryGetRequest)
        {
            var normalizeName = _keyNormalizer.NormalizeName(applicationSettingCategoryGetRequest.Name);

            var applicationSettingCategories = await _applicationSettingCategoryRepository.GetQueryable().Where(u => u.Name.StartsWith(normalizeName))
                .ToPagedListAsync(applicationSettingCategoryGetRequest.PageIndex, applicationSettingCategoryGetRequest.PageSize).ConfigureAwait(false);

            var applicationSettingCategoryGetResponse = _mapper.Map<IPagedList<Domain.ApplicationSettingCategory>, PagedResultDto<ApplicationSettingCategoryDto>>(applicationSettingCategories);
            return _serviceResponseHelper.SetSuccess(applicationSettingCategoryGetResponse);
        }

        public async Task<ServiceResponse<PagedResultDto<ApplicationSettingCategoryDto>>> GetApplicationSettingCategoryListAsync(PagedRequestDto pagedRequest)
        {
            var query = _applicationSettingCategoryRepository.GetQueryable();

            if (pagedRequest.Orders != null && pagedRequest.Orders.Any())
            {
                query = pagedRequest.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.Name);
            }

            var applicationSettingCategories = await query
                                                    .ToPagedListAsync(pagedRequest.PageIndex, pagedRequest.PageSize)
                                                    .ConfigureAwait(false);

            var applicationSettingCategoryGetResponse = _mapper.Map<IPagedList<Domain.ApplicationSettingCategory>, PagedResultDto<ApplicationSettingCategoryDto>>(applicationSettingCategories);
            return _serviceResponseHelper.SetSuccess(applicationSettingCategoryGetResponse);
        }
    }
}