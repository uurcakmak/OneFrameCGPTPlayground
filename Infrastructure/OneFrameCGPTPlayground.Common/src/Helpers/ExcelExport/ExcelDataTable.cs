// <copyright file="ExcelDataTable.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Data;

namespace OneFrameCGPTPlayground.Common.Helpers.ExcelExport
{
    public class ExcelDataTable : DataTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Should be returns array")]
        public string[] ColumnsToTake { get; set; }

        public string WorksheetName { get; set; }
    }
}