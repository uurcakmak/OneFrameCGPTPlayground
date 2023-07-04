// <copyright file="SaveMenuOrderDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts
{
    public class SaveMenuOrderDto
    {
        public List<SaveMenuOrderItemDto> MenuList { get; set; }
    }
}
