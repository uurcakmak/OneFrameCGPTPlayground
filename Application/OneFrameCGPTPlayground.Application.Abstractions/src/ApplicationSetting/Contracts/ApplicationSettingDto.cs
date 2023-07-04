// <copyright file="ApplicationSettingDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;

namespace OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting
{
    public class ApplicationSettingDto : IDto<Guid>
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public dynamic Value { get; set; }

        public string ValueType { get; set; }

        public bool IsStatic { get; set; }

        public string Status { get; set; }

        public Guid CategoryId { get; set; }
    }
}
