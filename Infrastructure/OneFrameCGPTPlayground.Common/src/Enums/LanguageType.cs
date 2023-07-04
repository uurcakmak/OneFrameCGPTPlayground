// <copyright file="LanguageType.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace OneFrameCGPTPlayground.Common.Enums
{
    /// <summary>
    /// Language Enum.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "for I18N.UICulture.TwoLetterISOLanguageName")]
    public enum LanguageType
    {
        [Description("English")]
        en = 1,

        [Description("Turkish")]
        tr = 2,

        [Description("Arabic")]
        ar = 3,
    }
}