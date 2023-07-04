// <copyright file="ConfirmationType.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OneFrameCGPTPlayground.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ConfirmationType
    {
        [Description("PhoneNumber")]
        PhoneNumber = 1,

        [Description("Email")]
        Email = 2,
    }
}