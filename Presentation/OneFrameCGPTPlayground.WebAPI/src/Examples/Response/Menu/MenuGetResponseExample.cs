// <copyright file="MenuGetResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.Menu;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class MenuGetResponseExample : IExamplesProvider<ServiceResponse<List<MenuResponse>>>
    {
        public ServiceResponse<List<MenuResponse>> GetExamples()
        {
            return new ServiceResponse<List<MenuResponse>>(new List<MenuResponse>
            {
               new MenuResponse
               {
                   Children = new List<MenuResponse>
                   {
                       new MenuResponse
                       {
                           Children = null,
                           Icon = "cio",
                           Id = 2,
                           Name = "sub 1",
                           ParentId = 1,
                           Url = "http",
                       },
                   },
                   Icon = "cio 2",
                   Id = 1,
                   Name = "sub 1",
                   ParentId = null,
                   Url = "http",
               },
            });
        }
    }
}