// <copyright file="ClaimTreeViewItemDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts
{
    public class ClaimTreeViewItemDto
    {
        public ClaimTreeViewItemDto()
        {
            Children = new List<ClaimTreeViewItemDto>();
        }

        public string Id { get; set; }

        public string Text { get; set; }

        public ClaimTreeViewItemStateInfoDto State { get; set; }

        public virtual List<ClaimTreeViewItemDto> Children { get; set; }
    }
}