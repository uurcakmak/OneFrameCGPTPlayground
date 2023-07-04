// <copyright file="ApplicationServiceBase.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace OneFrameCGPTPlayground.Application
{
    public class ApplicationServiceBase<TClass>
    {
        private readonly IServiceProvider _serviceProvider;

        private IKsI18N _i18N;

        private IKsStringLocalizer<TClass> _localize;

        private IMapper _mapper;

        private IServiceResponseHelper _serviceResponseHelper;

        protected ApplicationServiceBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected IKsI18N I18N
        {
            get
            {
                return _i18N ??= _serviceProvider.GetService<IKsI18N>();
            }
        }

        protected IKsStringLocalizer<TClass> Localize
        {
            get { return _localize ??= I18N.GetLocalizer<TClass>(); }
        }

        protected IMapper Mapper
        {
            get { return _mapper ??= _serviceProvider.GetService<IMapper>(); }
        }

        protected IServiceResponseHelper ServiceResponseHelper
        {
            get { return _serviceResponseHelper ??= _serviceProvider.GetService<IServiceResponseHelper>(); }
        }
    }
}
