// <copyright file="StartupPaths.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Helper
{
    public static class StartupPaths
    {
        public const string EndpointUrl = "/swagger/v1/swagger.json";
        public const string SwaggerPath = "/swagger-ui/";
        public const string StylesheetPath = SwaggerPath + "custom.css";
        public const string JavascriptPath = SwaggerPath + "custom.js";
    }
}