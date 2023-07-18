// <copyright file="MenuController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using Microsoft.AspNetCore.Mvc;
using OneFrameCGPTPlayground.Application.Abstractions.ChatGPT;
using OneFrameCGPTPlayground.Application.ChatGPT;
using OneFrameCGPTPlayground.WebAPI.Model.ChatGpt;
using System.Net.Mime;

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
            var apiKey = _configuration["ChatGPT:ApiKey"];
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

            if (response.IsSuccessful)
            {
                // Perform text diffing
                var diffBuilder = new InlineDiffBuilder(new Differ());
                var diffResult = diffBuilder.BuildDiffModel(sourceFileContent, targetFileContent);
                
                // Highlight changes in the text
                var highlightedText = string.Join("", diffResult.Lines.Select(GetHighlightedLine));

                response.Result = response.Result + System.Environment.NewLine + highlightedText;
            }

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

        static string GetHighlightedLine(DiffPiece line)
        {
            string highlightedLine;
            switch (line.Type)
            {
                case ChangeType.Inserted:
                    highlightedLine = $"<div class=\"bg-success\">{line.Text}</div>";
                    break;
                case ChangeType.Deleted:
                    highlightedLine = $"<div class=\"bg-danger\">{line.Text}</div>";
                    break;
                default:
                    highlightedLine = line.Text;
                    break;
            }
            return highlightedLine;
        }
    }
}