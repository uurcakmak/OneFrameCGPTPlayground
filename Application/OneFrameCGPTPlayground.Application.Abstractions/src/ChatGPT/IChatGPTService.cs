// <copyright file="IChatGPTService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.ChatGPT;

public interface IChatGPTService : IApplicationService
{
    Task<ServiceResponse<string>> Compare(string sourceContent, string targetContent);
}