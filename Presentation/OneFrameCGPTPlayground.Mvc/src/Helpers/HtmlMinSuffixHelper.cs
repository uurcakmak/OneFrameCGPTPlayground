// <copyright file="HtmlMinSuffixHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Helpers
{
    public class HtmlMinSuffixHelper
    {
        public HtmlMinSuffixHelper()
        {
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            MinSuffix = !isDevelopment ? ".min" : string.Empty;
        }

        public string MinSuffix { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "not needed")]
        public static implicit operator string(HtmlMinSuffixHelper minSuffix) => minSuffix.MinSuffix;
    }
}