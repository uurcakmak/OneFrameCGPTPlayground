// <copyright file="MenuTreeViewItemStateInfoDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts
{
    public class MenuTreeViewItemStateInfoDto
    {
        public bool Opened { get; set; }

        public bool Selected { get; set; }

        public bool Disabled { get; set; }
    }
}
