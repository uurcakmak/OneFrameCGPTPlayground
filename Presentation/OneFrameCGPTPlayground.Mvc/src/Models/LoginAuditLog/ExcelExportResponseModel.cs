// <copyright file="ExcelExportResponseModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.LoginAuditLog
{
    public class ExcelExportResponseModel
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] FileByteArray { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

        public string FileName { get; set; }
    }
}
