// <copyright file="EmailTemplateService.cs" company="KocSistem">
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
using Microsoft.EntityFrameworkCore;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.EmailTemplate
{
    public class EmailTemplateService : ApplicationCrudServiceAsync<Domain.EmailTemplate, EmailTemplateDto, Guid>, IEmailTemplateService
    {
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.EmailTemplate> _emailTemplateRepository;
        private readonly IKsStringLocalizer<EmailTemplateService> _localize;
        private readonly IServiceResponseHelper _serviceResponseHelper;

        public EmailTemplateService(IRepository<Domain.EmailTemplate> emailTemplateRepository, IMapper mapper, IDataManager dataManager, IKsI18N i18N, IServiceResponseHelper serviceResponseHelper, ILookupNormalizer keyNormalizer)
            : base(emailTemplateRepository, mapper, dataManager)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _mapper = mapper;
            _serviceResponseHelper = serviceResponseHelper;
            _localize = i18N.GetLocalizer<EmailTemplateService>();
            _keyNormalizer = keyNormalizer;
        }

        public async Task<ServiceResponse<PagedResultDto<EmailTemplateDto>>> GetEmailTemplateListAsync(PagedRequestDto pagedRequest)
        {
            var query = _emailTemplateRepository.GetQueryable(include: source => source.Include(c => c.Translations));

            if (pagedRequest.Orders != null && pagedRequest.Orders.Any())
            {
                query = pagedRequest.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.Name);
            }

            var emailTemplates = await query.ToPagedListAsync(pagedRequest.PageIndex, pagedRequest.PageSize).ConfigureAwait(false);

            var emailTemplateGetResponse = _mapper.Map<IPagedList<Domain.EmailTemplate>, PagedResultDto<EmailTemplateDto>>(emailTemplates);

            return _serviceResponseHelper.SetSuccess(emailTemplateGetResponse);
        }

        public async Task<ServiceResponse<EmailTemplateDto>> UpdateEmailTemplateAsync(EmailTemplateDto emailTemplateDto)
        {
            var emailTemplateToUpdate = await _emailTemplateRepository.GetFirstOrDefaultAsync(predicate: source => source.Id == emailTemplateDto.Id, include: source => source.Include(c => c.Translations)).ConfigureAwait(false);

            if (emailTemplateToUpdate == null || emailTemplateDto == null)
            {
                return _serviceResponseHelper.SetError<EmailTemplateDto>(null, _localize["EmailTemplateUpdateModelNotFound"], StatusCodes.Status400BadRequest, true);
            }

            foreach (var emailTemplateTranslate in emailTemplateDto.Translations)
            {
                var translationItem = emailTemplateToUpdate.Translations.FirstOrDefault(f => f.Language == emailTemplateTranslate.Language);
                if (translationItem == null)
                {
                    var newTranslation = Mapper.Map<Domain.EmailTemplateTranslation>(emailTemplateTranslate);
                    emailTemplateToUpdate.Translations.Add(newTranslation);
                }
                else
                {
                    translationItem.EmailContent = emailTemplateTranslate.EmailContent;
                    translationItem.Subject = emailTemplateTranslate.Subject;
                    translationItem.Reference = emailTemplateToUpdate;
                }
            }

            emailTemplateToUpdate.Bcc = emailTemplateDto.Bcc;
            emailTemplateToUpdate.Cc = emailTemplateDto.Cc;
            emailTemplateToUpdate.To = emailTemplateDto.To;
            await _emailTemplateRepository.UpdateAsync(emailTemplateToUpdate).ConfigureAwait(false);
            var result = _mapper.Map<Domain.EmailTemplate, EmailTemplateDto>(emailTemplateToUpdate);
            return _serviceResponseHelper.SetSuccess(result);
        }

        public async Task<ServiceResponse<EmailTemplateDto>> GetEmailTemplateByIdAsync(Guid id)
        {
            var emailTemplate = await _emailTemplateRepository.GetFirstOrDefaultAsync(predicate: source => source.Id == id, include: source => source.Include(c => c.Translations)).ConfigureAwait(false);

            if (emailTemplate == null)
            {
                return _serviceResponseHelper.SetError<EmailTemplateDto>(null, _localize["EmailTemplateNotFound"], StatusCodes.Status204NoContent, true);
            }

            var emailTemplateDto = _mapper.Map<Domain.EmailTemplate, EmailTemplateDto>(emailTemplate);
            return _serviceResponseHelper.SetSuccess(emailTemplateDto);
        }

        public async Task<ServiceResponse<EmailTemplateDto>> GetEmailTemplateByNameAsync(string emailTemplateName)
        {
            var emailTemplate = await _emailTemplateRepository.GetFirstOrDefaultAsync(predicate: source => source.Name == emailTemplateName, include: source => source.Include(c => c.Translations)).ConfigureAwait(false);

            if (emailTemplate == null)
            {
                return _serviceResponseHelper.SetError<EmailTemplateDto>(null, _localize["EmailTemplateNotFound"], StatusCodes.Status204NoContent, true);
            }

            var emailTemplateDto = _mapper.Map<Domain.EmailTemplate, EmailTemplateDto>(emailTemplate);
            return _serviceResponseHelper.SetSuccess(emailTemplateDto);
        }

        public async Task<ServiceResponse<PagedResultDto<EmailTemplateDto>>> SearchAsync(EmailTemplateSearchDto emailTemplateGetRequest)
        {
            var normalizeName = _keyNormalizer.NormalizeName(emailTemplateGetRequest.Name);

            var emailTemplates = await _emailTemplateRepository.GetQueryable(include: source => source.Include(c => c.Translations)).Where(u => u.Name.Contains(normalizeName))
                .ToPagedListAsync(emailTemplateGetRequest.PageIndex, emailTemplateGetRequest.PageSize).ConfigureAwait(false);

            var emailTemplateGetResponse = _mapper.Map<IPagedList<Domain.EmailTemplate>, PagedResultDto<EmailTemplateDto>>(emailTemplates);

            return _serviceResponseHelper.SetSuccess(emailTemplateGetResponse);
        }
    }
}