// <copyright file="MenuSaveMenuOrderRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.Menu;
using Swashbuckle.AspNetCore.Filters;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    internal class MenuSaveMenuOrderRequestExample : IExamplesProvider<SaveMenuOrderModel>
    {
        public SaveMenuOrderModel GetExamples()
        {
            return new SaveMenuOrderModel
            {
                MenuList = new List<SaveMenuOrderItemModel>
                {
                    new SaveMenuOrderItemModel
                    {
                        Id = 1,
                        ParentId = null,
                        OrderId = 0,
                    },
                    new SaveMenuOrderItemModel
                    {
                        Id = 2,
                        ParentId = 0,
                        OrderId = 1,
                    },
                },
            };
        }
    }
}
