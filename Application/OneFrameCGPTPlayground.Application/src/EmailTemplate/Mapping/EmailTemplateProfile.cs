// <copyright file="EmailTemplateProfile.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate.Contracts;
using KocSistem.OneFrame.Data.Relational;

namespace OneFrameCGPTPlayground.Application.EmailTemplate.Mappings
{
    /// <summary>
    ///  Definition EmailTemplate Entity AutoMapper Profiles.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            this.CreateMap<Domain.EmailTemplate, EmailTemplateDto>().ReverseMap();

            this.CreateMap<IPagedList<Domain.EmailTemplate>, PagedResultDto<EmailTemplateDto>>().ReverseMap();
        }
    }
}
