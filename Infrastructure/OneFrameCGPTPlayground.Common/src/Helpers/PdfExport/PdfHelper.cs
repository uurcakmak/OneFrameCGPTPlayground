// <copyright file="PdfHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using KocSistem.OneFrame.I18N;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OneFrameCGPTPlayground.Common.Helpers.PdfExport
{
    public static class PdfHelper
    {
        /// <summary>
        /// Exporting pdf
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="data">data</param>
        /// <param name="settings">pdf settings</param>
        /// <param name="localize">localize object for multilanguage</param>
        /// <returns>pdf file as byte array</returns>
        public static byte[] PdfExport<T>(this List<T> data, PdfExportDocumentSettings settings, IKsStringLocalizer<object> localize)
        {
            byte[] result = null;
            if (data == null)
            {
                return result;
            }

            var properties = data.GetType().GetGenericArguments().Single().GetProperties();
            var pageSize = settings?.PageSize ?? PageSize.A4;
            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA, settings?.EncodingType ?? "CP1254");
            var bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD, settings?.EncodingType ?? "CP1254");

            using (var stream = new MemoryStream())
            {
                using var pdfWriter = new PdfWriter(stream);
                using var pdfDoc = new PdfDocument(pdfWriter);
                using var doc = new Document(pdfDoc, pageSize, false);

                if (!string.IsNullOrEmpty(settings?.Date))
                {
                    _ = doc.Add(AddDate(settings.Date, font));
                }

                if (!string.IsNullOrEmpty(settings?.Title))
                {
                    _ = doc.Add(new Paragraph(settings.Title)
                            .SetFont(bold)
                            .SetFontSize(14).SetTextAlignment(TextAlignment.CENTER));
                }

                Table table = CreateTable(data, localize, properties, font, bold);

                _ = doc.Add(table);
                doc.Flush();

                if (settings != null && settings.ShowPageNumber)
                {
                    AddPageNumber(doc, pdfDoc, pageSize);
                }

                doc.Close();
                result = stream.ToArray();
                return result;
            }
        }

        private static Table CreateTable<T>(List<T> data, IKsStringLocalizer<object> localize, PropertyInfo[] properties, PdfFont font, PdfFont bold)
        {
            var columnCount = properties.Length;
            var rowCount = data.Count;

            var table = new Table(columnCount);
            _ = table.SetWidth(UnitValue.CreatePercentValue(100));

            var headerCell = new Cell();
            _ = headerCell.SetFont(bold);
            _ = headerCell.SetTextAlignment(TextAlignment.CENTER);

            var propertyDescption = string.Empty;
            DescriptionAttribute description = null;
            for (var i = 0; i < columnCount; i++)
            {
                var attribute = properties[i].GetCustomAttribute(typeof(DescriptionAttribute), true);

                if (attribute is null)
                {
                    propertyDescption = localize[properties[i].Name].ResourceNotFound ? properties[i].Name : localize[properties[i].Name];
                }
                else
                {
                    description = (DescriptionAttribute)attribute;
                    propertyDescption = localize[description.Description].ResourceNotFound ? description.Description : localize[description.Description];
                }

                var cell = headerCell.Clone(false);
                _ = cell.Add(new Paragraph(propertyDescption));
                _ = table.AddHeaderCell(cell);
            }

            object row;
            Type type;
            for (var i = 0; i < rowCount; i++)
            {
                row = data[i];
                type = row.GetType();
                for (var j = 0; j < columnCount; j++)
                {
                    _ = table.AddCell(type.GetProperties()[j].GetValue(row, null)?.ToString()).SetFont(font);
                }
            }

            return table;
        }

        private static void AddPageNumber(Document document, PdfDocument pdfDocument, PageSize pageSize)
        {
            var coordX = ((pageSize.GetLeft() + document.GetLeftMargin())
                          + (pageSize.GetRight() - document.GetRightMargin())) / 2;
            var footerY = document.GetBottomMargin();

            var numberOfPages = pdfDocument.GetNumberOfPages();
            for (var i = 1; i <= numberOfPages; i++)
            {
                _ = document.ShowTextAligned(
                new Paragraph(i + " / " + numberOfPages), x: coordX, y: footerY, pageNumber: i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
            }
        }

        private static Paragraph AddDate(string date, PdfFont font)
        {
            var reportDate = new Paragraph(date);
            _ = reportDate.SetTextAlignment(TextAlignment.RIGHT).SetFont(font);
            return reportDate;
        }
    }
}
