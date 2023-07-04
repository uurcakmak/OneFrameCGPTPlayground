// <copyright file="IMenuTranslationService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.MenuTranslation.Contracts;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects;

namespace OneFrameCGPTPlayground.Application.Abstractions.MenuTranslation
{
    /// <summary>
    /// Men uTranslation Service interface.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IMenuTranslationService : IApplicationCrudServiceAsync<Domain.MenuTranslation, MenuTranslationDto, int>, IApplicationService
    {
    }
}