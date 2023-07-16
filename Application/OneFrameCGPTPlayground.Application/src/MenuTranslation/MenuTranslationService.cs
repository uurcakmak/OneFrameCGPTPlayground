// <copyright file="MenuTranslationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Data;
using KocSistem.OneFrame.Data.Relational;
using OneFrameCGPTPlayground.Application.Abstractions.MenuTranslation;
using OneFrameCGPTPlayground.Application.Abstractions.MenuTranslation.Contracts;

namespace OneFrameCGPTPlayground.Application.MenuTranslation
{
    /// <summary>
    /// Menu Translation Service.
    /// </summary>
    /// <seealso cref="IMenuTranslationService" />
    public class MenuTranslationService : ApplicationCrudServiceAsync<Domain.MenuTranslation, MenuTranslationDto, int>, IMenuTranslationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuTranslationService"/> class.
        /// </summary>
        /// <param name="menuTranslationRepository">The menu translation repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="dataManager">The data manager.</param>
        public MenuTranslationService(IRepository<Domain.MenuTranslation> menuTranslationRepository, IMapper mapper, IDataManager dataManager)
            : base(menuTranslationRepository, mapper, dataManager)
        {
        }
    }
}