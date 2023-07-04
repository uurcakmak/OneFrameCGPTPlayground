﻿// <copyright file="IApplicationSettingConfig.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace OneFrameCGPTPlayground.Common.Helpers.ApplicationSetting
{
    public interface IApplicationSettingConfig
    {
        public List<string> CategoryNameList { get; set; }
    }
}