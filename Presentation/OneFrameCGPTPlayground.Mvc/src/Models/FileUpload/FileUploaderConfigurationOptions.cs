// <copyright file="FileUploaderConfigurationOptions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace OneFrameCGPTPlayground.Mvc.Models.FileUpload
{
    public class FileUploaderConfigurationOptions
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("withCredentials")]
        public bool WithCredentials { get; set; }

        [JsonProperty("timeout")]
        public int Timeout { get; set; }

        [JsonProperty("parallelUploads")]
        public int ParallelUploads { get; set; }

        [JsonProperty("uploadMultiple")]
        public bool UploadMultiple { get; set; }

        [JsonProperty("chunking")]
        public bool Chunking { get; set; }

        [JsonProperty("forceChunking")]
        public bool ForceChunking { get; set; }

        [JsonProperty("chunkSize")]
        public int ChunkSize { get; set; }

        [JsonProperty("parallelChunkUploads")]
        public bool ParallelChunkUploads { get; set; }

        [JsonProperty("retryChunks")]
        public bool RetryChunks { get; set; }

        [JsonProperty("retryChunksLimit")]
        public int RetryChunksLimit { get; set; }

        [JsonProperty("maxFilesize")]
        public int MaxFilesize { get; set; }

        [JsonProperty("paramName")]
        public string ParamName { get; set; }

        [JsonProperty("createImageThumbnails")]
        public bool CreateImageThumbnails { get; set; }

        [JsonProperty("maxThumbnailFilesize")]
        public int MaxThumbnailFilesize { get; set; }

        [JsonProperty("thumbnailWidth")]
        public int ThumbnailWidth { get; set; }

        [JsonProperty("thumbnailHeight")]
        public int ThumbnailHeight { get; set; }

        [JsonProperty("thumbnailMethod")]
        public string ThumbnailMethod { get; set; }

        [JsonProperty("resizeWidth")]
        public int ResizeWidth { get; set; }

        [JsonProperty("resizeHeight")]
        public int ResizeHeight { get; set; }

        [JsonProperty("resizeMimeType")]
        public string ResizeMimeType { get; set; }

        [JsonProperty("resizeQuality")]
        public double ResizeQuality { get; set; }

        [JsonProperty("resizeMethod")]
        public string ResizeMethod { get; set; }

        [JsonProperty("filesizeBase")]
        public int FilesizeBase { get; set; }

        [JsonProperty("maxFiles")]
        public int MaxFiles { get; set; }

        [JsonProperty("headers")]
        public string Headers { get; set; }

        [JsonProperty("clickable")]
        public bool Clickable { get; set; }

        [JsonProperty("ignoreHiddenFiles")]
        public bool IgnoreHiddenFiles { get; set; }

        [JsonProperty("acceptedFiles")]
        public string AcceptedFiles { get; set; }

        [JsonProperty("acceptedMimeTypes")]
        public string AcceptedMimeTypes { get; set; }

        [JsonProperty("autoProcessQueue")]
        public bool AutoProcessQueue { get; set; }

        [JsonProperty("autoQueue")]
        public bool AutoQueue { get; set; }

        [JsonProperty("addRemoveLinks")]
        public bool AddRemoveLinks { get; set; }

        [JsonProperty("previewsContainer")]
        public string PreviewsContainer { get; set; }

        [JsonProperty("hiddenInputContainer")]
        public string HiddenInputContainer { get; set; }

        [JsonProperty("capture")]
        public string Capture { get; set; }

        [JsonProperty("renameFilename")]
        public string RenameFilename { get; set; }

        [JsonProperty("renameFile")]
        public string RenameFile { get; set; }

        [JsonProperty("forceFallback")]
        public bool ForceFallback { get; set; }
    }
}