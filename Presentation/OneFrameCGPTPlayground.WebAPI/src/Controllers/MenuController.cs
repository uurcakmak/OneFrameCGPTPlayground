// <copyright file="MenuController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Menu;
using OneFrameCGPTPlayground.Application.Abstractions.Menu.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.Menu;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("menu")]
    [Produces(MediaTypeNames.Application.Json)]
    public class MenuController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns menu items.
        /// </summary>
        /// <returns>Returns list of MenuModel items. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>[HttpGet]
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<List<MenuResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(MenuGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _menuService.GetUserMenuListAsync().ConfigureAwait(false);

            if (!result.IsSuccessful || result.Result == null)
            {
                return Ok(result);
            }

            var menuResponse = _mapper.Map<IList<UserMenuDto>, IList<MenuResponse>>(result.Result.ToList());

            return Ok(Success(menuResponse));
        }

        /// <summary>
        /// Get tree in menu.
        /// </summary>
        /// <returns>Returns list of MenuModel items. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("tree")]
        [Authorize(Policy = KsPermissionPolicy.ManagementMenuList)]
        [ProducesResponseType(typeof(ServiceResponse<List<MenuTreeViewItem>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(MenuGetMenuTreeResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetMenuTreeAsync()
        {
            var result = await _menuService.GetMenuTreeAsync().ConfigureAwait(false);
            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var mappingResult = _mapper.Map<List<MenuTreeViewItem>>(result.Result);
            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Saves the menu order.
        /// </summary>
        /// <param name="model">SaveMenuOrderModel Model</param>
        /// <returns>Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("order")]
        [Authorize(Policy = KsPermissionPolicy.ManagementMenuEdit)]
        [SwaggerRequestExample(typeof(SaveMenuOrderModel), typeof(MenuSaveMenuOrderRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SaveMenuOrderAsync([FromBody] SaveMenuOrderModel model)
        {
            var mappingModel = _mapper.Map<SaveMenuOrderDto>(model);
            var result = await _menuService.SaveMenuOrderAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}