// <copyright file="RoleGetRoleClaimsTreeResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Services;
using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class RoleGetRoleClaimsTreeResponseExample : IExamplesProvider<ServiceResponse<List<ClaimTreeViewItem>>>
    {
        public ServiceResponse<List<ClaimTreeViewItem>> GetExamples()
        {
            return new ServiceResponse<List<ClaimTreeViewItem>>(new List<ClaimTreeViewItem>
            {
               new ClaimTreeViewItem
               {
                   Id = Guid.NewGuid().ToString(),
                   State = new ClaimTreeViewItemStateInfo
                   {
                       Disabled = false,
                       Opened = true,
                       Selected = true,
                   },
                   Text = "Claim Name 1",
                   Children = new List<ClaimTreeViewItem>
                   {
                        new ClaimTreeViewItem
                        {
                            Id = Guid.NewGuid().ToString(),
                            State = new ClaimTreeViewItemStateInfo
                            {
                                Disabled = false,
                                Opened = true,
                                Selected = true,
                            },
                            Children = null,
                            Text = "Claim Name 2",
                        },
                   },
               },
            });
        }
    }
}