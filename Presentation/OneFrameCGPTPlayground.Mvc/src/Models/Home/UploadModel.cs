// <copyright file="IndexModel.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.Home
{
    public class UploadModel
    {
        public IFormFile SourceFile { get; set; }
        
        public IFormFile TargetFile { get; set; }
    }
}
