// <copyright file="ConfigurationGetFileUploaderOptionsResponseExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.FileUpload;
using KocSistem.OneFrame.DesignObjects.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Response
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class ConfigurationGetFileUploaderOptionsResponseExample : IExamplesProvider<ServiceResponse<FileUploaderConfigurationOptions>>
    {
        public ServiceResponse<FileUploaderConfigurationOptions> GetExamples()
        {
            return new ServiceResponse<FileUploaderConfigurationOptions>(new FileUploaderConfigurationOptions
            {
                Url = "http",
                Method = "method",
                WithCredentials = false,
                Timeout = 0,
                ParallelUploads = 0,
                UploadMultiple = false,
                Chunking = false,
                ForceChunking = false,
                ChunkSize = 0,
                ParallelChunkUploads = false,
                RetryChunks = false,
                RetryChunksLimit = 0,
                MaxFilesize = 0,
                ParamName = "ParamName",
                CreateImageThumbnails = false,
                MaxThumbnailFilesize = 0,
                ThumbnailWidth = 0,
                ThumbnailHeight = 0,
                ThumbnailMethod = "ThumbnailMethod",
                ResizeWidth = 0,
                ResizeHeight = 0,
                ResizeMimeType = "ResizeMimeType",
                ResizeQuality = 0,
                ResizeMethod = "ResizeMethod",
                FilesizeBase = 0,
                MaxFiles = 0,
                Headers = "Headers",
                Clickable = false,
                IgnoreHiddenFiles = false,
                AcceptedFiles = "AcceptedFiles",
                AcceptedMimeTypes = "AcceptedMimeTypes",
                AutoProcessQueue = false,
                AutoQueue = false,
                AddRemoveLinks = false,
                PreviewsContainer = "PreviewsContainer",
                HiddenInputContainer = "HiddenInputContainer",
                Capture = "Capture",
                RenameFilename = "RenameFilename",
                RenameFile = "RenameFile",
                ForceFallback = false,
            });
        }
    }
}