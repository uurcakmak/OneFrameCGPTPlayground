// <copyright file="LanguageService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Language;
using OneFrameCGPTPlayground.Application.Abstractions.Language.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Language
{
    public class LanguageService : ApplicationCrudServiceAsync<Domain.Language, LanguageDto, Guid>, ILanguageService
    {
        private readonly IRepository<Domain.Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly IKsStringLocalizer<LanguageService> _localize;
        private readonly IServiceResponseHelper _serviceResponseHelper;
        private readonly ILookupNormalizer _keyNormalizer;

        public LanguageService(IRepository<Domain.Language> languageRepository, IMapper mapper, IDataManager dataManager, IKsI18N i18N, IServiceResponseHelper serviceResponseHelper, ILookupNormalizer keyNormalizer)
           : base(languageRepository, mapper, dataManager)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
            _localize = i18N.GetLocalizer<LanguageService>();
            _serviceResponseHelper = serviceResponseHelper;
            _keyNormalizer = keyNormalizer;
        }

        public async Task<ServiceResponse<PagedResultDto<LanguageDto>>> GetLanguageListAsync(PagedRequestDto pagedRequest)
        {
            var query = _languageRepository.GetQueryable();

            if (pagedRequest.Orders != null && pagedRequest.Orders.Any())
            {
                query = pagedRequest.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.InsertedDate);
            }

            var languages = await query.ToPagedListAsync(pagedRequest.PageIndex, pagedRequest.PageSize).ConfigureAwait(false);

            var languagesGetResponse = _mapper.Map<IPagedList<Domain.Language>, PagedResultDto<LanguageDto>>(languages);
            return _serviceResponseHelper.SetSuccess(languagesGetResponse);
        }

        public async Task<ServiceResponse<PagedResultDto<LanguageDto>>> SearchAsync(LanguageSearchDto languageGetRequest)
        {
            var normalizeName = _keyNormalizer.NormalizeName(languageGetRequest.Key);

            var languages = await _languageRepository.GetQueryable().Where(u => u.Name.Contains(normalizeName))
                .ToPagedListAsync(languageGetRequest.PageIndex, languageGetRequest.PageSize).ConfigureAwait(false);

            var languageGetResponse = _mapper.Map<IPagedList<Domain.Language>, PagedResultDto<LanguageDto>>(languages);
            return _serviceResponseHelper.SetSuccess(languageGetResponse);
        }

        public override async Task<ServiceResponse<LanguageDto>> CreateAsync(LanguageDto model)
        {
            if (model.IsDefault)
            {
                var defaultLanguage = await _languageRepository.GetFirstOrDefaultAsync(predicate: x => x.IsDefault).ConfigureAwait(false);

                if (defaultLanguage is not null)
                {
                    defaultLanguage.IsDefault = false;
                    await _languageRepository.UpdateAsync(defaultLanguage).ConfigureAwait(false);
                }
            }

            var entityModel = _mapper.Map<LanguageDto, Domain.Language>(model);
            await _languageRepository.AddAsync(entityModel).ConfigureAwait(false);
            var returnDto = _mapper.Map<Domain.Language, LanguageDto>(entityModel);

            return _serviceResponseHelper.SetSuccess(returnDto);
        }

        public override async Task<ServiceResponse<LanguageDto>> UpdateAsync(LanguageDto model)
        {
            if (model.IsDefault && !model.IsActive)
            {
                return _serviceResponseHelper.SetError<LanguageDto>(null, _localize["DefaultLanguageCannotBePassive"], StatusCodes.Status400BadRequest, true);
            }

            if (model.IsDefault)
            {
                var defaultLanguage = await _languageRepository.GetFirstOrDefaultAsync(predicate: x => x.IsDefault && x.Id != model.Id).ConfigureAwait(false);

                if (defaultLanguage is not null)
                {
                    defaultLanguage.IsDefault = false;
                    await _languageRepository.UpdateAsync(defaultLanguage).ConfigureAwait(false);
                }
            }

            var language = await _languageRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == model.Id).ConfigureAwait(false);

            if (language is null)
            {
                return _serviceResponseHelper.SetError<LanguageDto>(null, _localize["LanguageNotFound"], StatusCodes.Status204NoContent, true);
            }

            var entity = _mapper.Map(model, language);

            await _languageRepository.UpdateAsync(entity).ConfigureAwait(false);

            return _serviceResponseHelper.SetSuccess(_mapper.Map<Domain.Language, LanguageDto>(entity));
        }

        public override async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var entity = await _languageRepository.GetFirstOrDefaultAsync(predicate: x => x.Id == id).ConfigureAwait(false);

            if (entity is null)
            {
                return _serviceResponseHelper.SetError<LanguageDto>(null, _localize["LanguageNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (entity.IsDefault)
            {
                return _serviceResponseHelper.SetError<LanguageDto>(null, _localize["DefaultLanguageCannotBeDelete"], StatusCodes.Status400BadRequest, true);
            }

            await _languageRepository.DeleteAsync(entity).ConfigureAwait(false);

            return _serviceResponseHelper.SetSuccess();
        }
    }
}
