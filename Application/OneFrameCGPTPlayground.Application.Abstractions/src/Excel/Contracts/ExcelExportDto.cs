// <copyright file="ExcelExportDto.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Application.Abstractions.Excel.Contracts
{
    public class ExcelExportDto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Should be returns array")]
        public byte[] FileByteArray { get; set; }

        public string FileName { get; set; }
    }
}