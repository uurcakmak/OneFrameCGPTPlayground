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

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("chatGpt")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ChatGptController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public ChatGptController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        /// <summary>
        /// Compares two text based files using ChatGPT.
        /// </summary>
        [HttpPost("compare")]
        public async Task<IActionResult> CompareAsync([FromForm] UploadModel model)
        {
            return Ok(new ServiceResponse());
        }
    }
}