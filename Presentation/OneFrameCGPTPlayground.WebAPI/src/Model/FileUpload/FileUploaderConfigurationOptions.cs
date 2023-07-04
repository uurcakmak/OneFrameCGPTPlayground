// <copyright file="FileUploaderConfigurationOptions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel;

namespace OneFrameCGPTPlayground.WebAPI.Model.FileUpload
{
    public class FileUploaderConfigurationOptions
    {
        public FileUploaderConfigurationOptions()
        {
            Method = "post";
            Timeout = 30000;
            ParallelUploads = 1;
            ChunkSize = 2000000;
            RetryChunksLimit = 3;
            MaxFilesize = 256;
            ParamName = "file";
            MaxThumbnailFilesize = 10;
            ThumbnailWidth = 120;
            ThumbnailHeight = 120;
            ThumbnailMethod = "crop";
            ResizeQuality = 0.8;
            ResizeMethod = "contain";
            FilesizeBase = 1000;
            HiddenInputContainer = "body";
            CreateImageThumbnails = true;
            Clickable = true;
            IgnoreHiddenFiles = true;
            AutoProcessQueue = true;
            AutoQueue = true;
        }

        [DefaultValue(null)]
        public string Url { get; set; }

        public string Method { get; set; }

        public bool WithCredentials { get; set; }

        public int Timeout { get; set; }

        public int ParallelUploads { get; set; }

        public bool UploadMultiple { get; set; }

        public bool Chunking { get; set; }

        public bool ForceChunking { get; set; }

        public int ChunkSize { get; set; }

        public bool ParallelChunkUploads { get; set; }

        public bool RetryChunks { get; set; }

        public int RetryChunksLimit { get; set; }

        public int MaxFilesize { get; set; }

        public string ParamName { get; set; }

        public bool CreateImageThumbnails { get; set; }

        public int MaxThumbnailFilesize { get; set; }

        public int ThumbnailWidth { get; set; }

        public int ThumbnailHeight { get; set; }

        public string ThumbnailMethod { get; set; }

        [DefaultValue(null)]
        public int ResizeWidth { get; set; }

        [DefaultValue(null)]
        public int ResizeHeight { get; set; }

        [DefaultValue(null)]
        public string ResizeMimeType { get; set; }

        public double ResizeQuality { get; set; }

        public string ResizeMethod { get; set; }

        public int FilesizeBase { get; set; }

        [DefaultValue(null)]
        public int MaxFiles { get; set; }

        [DefaultValue(null)]
        public string Headers { get; set; }

        public bool Clickable { get; set; }

        public bool IgnoreHiddenFiles { get; set; }

        [DefaultValue(null)]
        public string AcceptedFiles { get; set; }

        [DefaultValue(null)]
        public string AcceptedMimeTypes { get; set; }

        public bool AutoProcessQueue { get; set; }

        public bool AutoQueue { get; set; }

        public bool AddRemoveLinks { get; set; }

        [DefaultValue(null)]
        public string PreviewsContainer { get; set; }

        public string HiddenInputContainer { get; set; }

        [DefaultValue(null)]
        public string Capture { get; set; }

        [DefaultValue(null)]
        public string RenameFilename { get; set; }

        [DefaultValue(null)]
        public string RenameFile { get; set; }

        public bool ForceFallback { get; set; }
    }
}