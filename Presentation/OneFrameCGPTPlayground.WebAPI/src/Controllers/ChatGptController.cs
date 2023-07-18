// <copyright file="MenuController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Mvc;
using OneFrameCGPTPlayground.Application.Abstractions.Menu;
using OneFrameCGPTPlayground.WebAPI.Model.ChatGpt;
using System.Net.Mime;
using OneFrameCGPTPlayground.Application.Abstractions.ChatGPT;
using OneFrameCGPTPlayground.Application.ChatGPT;
using System.IO;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("chatGpt")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ChatGptController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IChatGPTService _chatGptService;

        public ChatGptController(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            var apiKey =_configuration["ChatGPT:ApiKey"];
            _chatGptService = new ChatGPTService(apiKey);
        }

        /// <summary>
        /// Compares two text based files using ChatGPT.
        /// </summary>
        [HttpPost("compare")]
        public async Task<IActionResult> CompareAsync([FromForm] UploadModel model)
        {
            var sourceFileContent = await ReadFileContent(model.SourceFile).ConfigureAwait(false);
            var targetFileContent = await ReadFileContent(model.TargetFile).ConfigureAwait(false);

            var response = await _chatGptService.Compare(sourceFileContent, targetFileContent).ConfigureAwait(false);

            return Ok(response);
        }

        private async Task<string> ReadFileContent(IFormFile file)
        {
            var ms = new MemoryStream();
            await file.CopyToAsync(ms).ConfigureAwait(false);
            ms.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(ms);
            string text = await reader.ReadToEndAsync().ConfigureAwait(false);

            return text;
        }
    }
}