// <copyright file="ApplicationSettingCategory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using System;
using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Domain
{
    public class ApplicationSettingCategory : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<ApplicationSetting> ApplicationSettings { get; }
    }
}
