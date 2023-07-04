// <copyright file="EmailTemplateProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;
using OneFrameCGPTPlayground.Common.Helpers.AutoMapper;
using OneFrameCGPTPlayground.WebAPI.Model.EmailTemplate;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;

namespace OneFrameCGPTPlayground.WebAPI.Mappings
{
    /// <summary>
    ///  Definition Menu Dto AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class EmailTemplateProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplateProfile"/> class.
        /// </summary>
        public EmailTemplateProfile()
        {
            this.CreateMap<EmailTemplateDto, EmailTemplateGetResponse>().ReverseMap();
            this.CreateMap<EmailTemplateDto, EmailTemplateListResponse>().ToTimeZone(x => x.UpdatedDate).ReverseMap();
            this.CreateMap<EmailTemplateDto, EmailTemplateModel>().ReverseMap();
            this.CreateMap<SendEmailRequest, EmailTemplateSendEmailDto>().ReverseMap();
            this.CreateMap<PagedResultDto<EmailTemplateDto>, PagedResult<EmailTemplateGetResponse>>().ReverseMap();
            this.CreateMap<PagedResultDto<EmailTemplateDto>, PagedResult<EmailTemplateListResponse>>().ReverseMap();

            this.CreateMap<EmailTemplatePostRequest, EmailTemplateDto>().ReverseMap();
            this.CreateMap<EmailTemplatePutRequest, EmailTemplateDto>().ReverseMap();
            this.CreateMap<EmailTemplateDto, EmailTemplateGetWithTranslatesResponse>().ReverseMap();
            this.CreateMap<EmailTemplateTranslationsModel, EmailTemplateTranslationDto>().ReverseMap();
            this.CreateMap<EmailTemplateSearchRequest, EmailTemplateSearchDto>().ReverseMap();
        }
    }
}