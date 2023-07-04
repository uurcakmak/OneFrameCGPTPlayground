// <copyright file="SaveMenuOrderItemDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts
{
    public class SaveMenuOrderItemDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int OrderId { get; set; }
    }
}
