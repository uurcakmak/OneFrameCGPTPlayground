// <copyright file="HtmlRtlSuffixHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Globalization;

namespace OneFrameCGPTPlayground.Mvc.Helpers
{
    public class HtmlRtlSuffixHelper
    {
        public HtmlRtlSuffixHelper()
        {
            IsRightToLeft = CultureInfo.DefaultThreadCurrentCulture.TextInfo.IsRightToLeft;
            RtlFileSuffix = IsRightToLeft ? ".rtl" : string.Empty;
        }

        public bool IsRightToLeft { get; set; }

        public string RtlFileSuffix { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "not needed")]
        public static implicit operator bool(HtmlRtlSuffixHelper csHtmlHelper) => csHtmlHelper.IsRightToLeft;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "not needed")]
        public static implicit operator string(HtmlRtlSuffixHelper csHtmlHelper) => csHtmlHelper.RtlFileSuffix;
    }
}