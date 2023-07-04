// <copyright file="ApplicationSettingResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model.ApplicationSetting
{
    public class ApplicationSettingResponse
    {
        public Guid Id { get; set; }

        public string Key { get; set; }

        public dynamic Value { get; set; }

        public string ValueType { get; set; }

        public bool IsStatic { get; set; }

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Status { get; set; }
    }
}
