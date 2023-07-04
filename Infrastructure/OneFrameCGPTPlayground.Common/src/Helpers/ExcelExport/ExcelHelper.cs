// <copyright file="ExcelHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using ClosedXML.Excel;
using ClosedXML.Graphics;
using KocSistem.OneFrame.I18N;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace OneFrameCGPTPlayground.Common.Helpers.ExcelExport
{
    public static class ExcelHelper
    {
        /// <summary>
        /// Get Column Headers and To The Data Table
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="data">Data table</param>
        /// <param name="heading">Head</param>
        /// <param name="showRowNo">Show Row No</param>
        /// <param name="columnsToTake">Columns To Take</param>
        /// <returns>excel file as byte array</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "no needed")]
        public static byte[] Export<T>(this List<T> data, IKsStringLocalizer<object> localize, string heading = "Sheet1", bool showRowNo = false, params string[] columnsToTake)
        {
            if (columnsToTake == null || !columnsToTake.Any())
            {
                columnsToTake = GetColumnHeaders<T>(localize);
            }

            return Export(data.ToTheDataTable(localize), heading, showRowNo, columnsToTake);
        }

        /// <summary>
        /// Get Column Headers and To The Data Table
        /// </summary>
        /// <typeparam name="TKey">Key</typeparam>
        /// <typeparam name="TValue">Value</typeparam>
        /// <param name="data">Data table</param>
        /// <param name="heading">Head</param>
        /// <param name="showRowNo">Show Row No</param>
        /// <param name="columnsToTake">Columns To Take</param>
        /// <returns>excel file as byte array</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "no needed")]
        public static byte[] Export<TKey, TValue>(this List<Dictionary<TKey, TValue>> data, IKsStringLocalizer<object> localize, string heading = "Sheet1", bool showRowNo = false, params string[] columnsToTake)
        {
            if (columnsToTake == null || !columnsToTake.Any())
            {
                columnsToTake = GetColumnHeaders(data.FirstOrDefault(), localize);
            }

            return Export(data.ToTheDataTable(localize), heading, showRowNo, columnsToTake);
        }

        /// <summary>
        /// Exporting excel
        /// </summary>
        /// <param name="dataTable">Data table</param>
        /// <param name="heading">Head</param>
        /// <param name="showRowNo">Show Row No</param>
        /// <param name="columnsToTake">Columns To Take</param>
        /// <returns>excel file as byte array</returns>
        public static byte[] Export(this ExcelDataTable dataTable, string heading = "Sheet1", bool showRowNo = false, params string[] columnsToTake)
        {
            LoadOptions.DefaultGraphicEngine = new DefaultGraphicEngine("DejaVu Sans");
            if (OperatingSystem.IsWindows())
            {
                LoadOptions.DefaultGraphicEngine = new DefaultGraphicEngine("Arial");
            }
            else if (OperatingSystem.IsMacOS())
            {
                LoadOptions.DefaultGraphicEngine = new DefaultGraphicEngine("Helvetica");
            }

            byte[] result = null;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(dataTable, heading);

                foreach (var item in worksheet.Tables)
                {
                    item.Theme = XLTableTheme.None;
                }

                if (showRowNo)
                {
                    dataTable = AddRowNumbers(dataTable);
                }

                worksheet = RemoveColumns(worksheet, dataTable, showRowNo, columnsToTake);
                var cells = worksheet.Range(1, 1, 1, columnsToTake.Length);

                cells.CellsUsed().ToList().ForEach(x => { x.Style.Font.Bold = true; });

                _ = worksheet.Columns().AdjustToContents();

                foreach (var item in worksheet.CellsUsed().ToList())
                {
                    if (!item.Value.IsBlank && !item.Value.IsError && item.Value.GetType() == typeof(DateTime))
                    {
                        item.Style.NumberFormat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                    }
                }

                using var msA = new MemoryStream();
                workbook.SaveAs(msA);
                result = msA.ToArray();
            }

            return result;
        }

        /// <summary>
        /// Get Column Headers
        /// </summary>
        /// <typeparam name="TKey">Generic type</typeparam>
        /// <typeparam name="TValue">Generic type</typeparam>
        /// <param name="data">Data</param>
        /// <returns>Get Column Headers For Export Excel </returns>
        public static string[] GetColumnHeaders<TKey, TValue>(Dictionary<TKey, TValue> data, IKsStringLocalizer<object> localize)
        {
            var columnHeaders = new List<string>();
            for (var i = 0; i < data.Count; i++)
            {
                var localizedColumnName = localize[data.ElementAt(i).Key.ToString()].ResourceNotFound ? data.ElementAt(i).Key.ToString() : localize[data.ElementAt(i).Key.ToString()];
                columnHeaders.Add(localizedColumnName);
            }

            return columnHeaders.ToArray();
        }

        /// <summary>
        /// To The Data Table
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="data">Data</param>
        /// <returns>To The Data Table </returns>
        public static ExcelDataTable ToTheDataTable<T>(this List<T> data, IKsStringLocalizer<object> localize)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));

            var excelDataTable = new ExcelDataTable();

            foreach (PropertyDescriptor property in properties)
            {
                var localizedColumnName = localize[property.DisplayName].ResourceNotFound ? property.DisplayName : localize[property.DisplayName];

                if (!excelDataTable.Columns.Contains(localizedColumnName))
                {
                    _ = excelDataTable.Columns.Add(localizedColumnName, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
                }
            }

            var values = new object[properties.Count];

            foreach (var item in data)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                _ = excelDataTable.Rows.Add(values);
            }

            return excelDataTable;
        }

        /// <summary>
        /// To The Data Table
        /// </summary>
        /// <typeparam name="TKey">Generic type</typeparam>
        /// <typeparam name="TValue">Generic type</typeparam>
        /// <param name="data">Data</param>
        /// <returns>To The Data Table </returns>
        public static ExcelDataTable ToTheDataTable<TKey, TValue>(this List<Dictionary<TKey, TValue>> data, IKsStringLocalizer<object> localize)
        {
            var excelDataTable = new ExcelDataTable();
            var first = data.FirstOrDefault();

            for (var i = 0; i < first.Count; i++)
            {
                var localizedColumnName = localize[first.ElementAt(i).Key.ToString()].ResourceNotFound ? first.ElementAt(i).Key.ToString() : localize[first.ElementAt(i).Key.ToString()];
                _ = excelDataTable.Columns.Add(localizedColumnName, Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue));
            }

            var values = new object[first.Count];

            foreach (var item in data)
            {
                for (var i = 0; i < item.Count; i++)
                {
                    values[i] = item.ElementAt(i).Value;
                }

                _ = excelDataTable.Rows.Add(values);
            }

            return excelDataTable;
        }

        /// <summary>
        /// Add Row Numbers
        /// </summary>
        /// <param name="dataTable">Data table</param>
        /// <returns>Add Row Numbers For Excel Export</returns>
        private static ExcelDataTable AddRowNumbers(ExcelDataTable dataTable)
        {
            var dataColumn = dataTable.Columns.Add("#", typeof(int));
            dataColumn.SetOrdinal(0);
            var index = 1;
            foreach (DataRow item in dataTable.Rows)
            {
                index++;
            }

            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                row[0] = index;
            }

            return dataTable;
        }

        /// <summary>
        /// Get Column Headers
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <returns>Get Column Headers For Export Excel </returns>
        private static string[] GetColumnHeaders<T>(IKsStringLocalizer<object> localize)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var columnHeaders = new List<string>();
            foreach (PropertyDescriptor property in properties)
            {
                if (!string.IsNullOrEmpty(property.DisplayName) && property.SerializationVisibility != DesignerSerializationVisibility.Hidden)
                {
                    var localizedColumnName = localize[property.DisplayName].ResourceNotFound ? property.DisplayName : localize[property.DisplayName];
                    columnHeaders.Add(localizedColumnName);
                }
            }

            return columnHeaders.ToArray();
        }

        /// <summary>
        /// Remove Columns
        /// </summary>
        /// <param name="worksheet">Worksheet</param>
        /// <param name="dataTable">Data table</param>
        /// <param name="showRowNo">Show Row No</param>
        /// <param name="columnsToTake">Columns To Take</param>
        /// <returns>Remove Columns For Export Excel </returns>
        private static IXLWorksheet RemoveColumns(IXLWorksheet worksheet, ExcelDataTable dataTable, bool showRowNo, params string[] columnsToTake)
        {
            var finishIndex = showRowNo ? 1 : 0;
            for (var i = dataTable.Columns.Count - 1; i >= finishIndex; i--)
            {
                if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                {
                    worksheet.Columns(i, i).Delete();
                }
            }

            return worksheet;
        }
    }
}