// <copyright file="PdfExportDocumentSettings.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using iText.Kernel.Geom;

namespace OneFrameCGPTPlayground.Common.Helpers.PdfExport
{
    public class PdfExportDocumentSettings
    {
        public PageSize PageSize { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public bool ShowPageNumber { get; set; }

        public string EncodingType { get; set; }
    }
}
