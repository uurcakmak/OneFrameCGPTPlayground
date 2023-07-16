// <copyright file="RoleController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Role;
using OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Domain;
using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.Role;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("roles")]
    [Produces(MediaTypeNames.Application.Json)]
    public class RoleController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public RoleController(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
        }

        /// <summary>
        /// Add claim to role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="roleClaimPostRequest">RoleClaimPostRequest Model.</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("{roleName}/claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleAddClaim)]
        [SwaggerRequestExample(typeof(RoleClaimPostRequest), typeof(RoleClaimPostRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> AddClaimToRoleAsync(string roleName, [FromBody] RoleClaimPostRequest roleClaimPostRequest)
        {
            var mappingModel = _mapper.Map<RoleClaimPostRequest, RoleClaimDto>(roleClaimPostRequest);
            mappingModel.RoleName = roleName;

            var result = await _roleService.AddClaimToRoleAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Add user to role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="username">Username</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("{roleName}/users/{username}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleAddUser)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> AddUserToRoleAsync(string roleName, string username)
        {
            var result = await _roleService.AddUserToRoleAsync(roleName, username).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                if (result.Error.Code == StatusCodes.Status400BadRequest)
                {
                    return BadRequest(result);
                }
                else
                {
                    return Ok(result);
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete application role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{roleName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleDelete)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> DeleteAsync(string roleName)
        {
            var result = await _roleService.DeleteAsync(roleName).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get application roles.
        /// </summary>
        /// <returns>Return list of ApplicationRole items. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        [ProducesResponseType(typeof(ServiceResponse<List<ApplicationRoleModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public IActionResult Get()
        {
            var result = _roleService.GetApplicationRoles();

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var mappingResult = _mapper.Map<List<ApplicationRoleModel>>(result.Result);

            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Get application roles.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching roles with paging properties.</param>
        /// <returns>Return list of ApplicationRole items. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<RoleGetResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleGetListResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingModel = _mapper.Map<PagedRequest, PagedRequestDto>(pagedRequest);
            var result = await _roleService.GetApplicationRolesAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var roleGetResponse = _mapper.Map<PagedResultDto<ApplicationRoleDto>, PagedResult<RoleGetResponse>>(result.Result);
            return Ok(Success(roleGetResponse));
        }

        /// <summary>
        /// Get application role by role name.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>Returns ApplicationRole item. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{roleName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        [ProducesResponseType(typeof(ServiceResponse<RoleGetWithTranslatesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleGetWithRoleNameResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync(string roleName)
        {
            var result = await _roleService.GetRoleByNameAsync(roleName).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var roleGetResponse = _mapper.Map<RoleGetWithTranslatesResponse>(result.Result);
            return Ok(Success(roleGetResponse));
        }

        /// <summary>
        /// Get claims in role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>Returns claim list. Returns ClaimResponseModel items. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{roleName}/claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleClaimList)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClaimResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleGetClaimsInRoleResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetClaimsInRoleAsync(string roleName)
        {
            var result = await _roleService.GetClaimsInRoleAsync(roleName).ConfigureAwait(false);
            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var mappingResult = _mapper.Map<List<ClaimResponse>>(result.Result);
            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Get claims tree in role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>Returns role claim tree. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("role-claims-tree/{roleName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleClaimList)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClaimTreeViewItem>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleGetRoleClaimsTreeResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetRoleClaimsTreeAsync(string roleName)
        {
            var result = await _roleService.GetRoleClaimsTreeAsync(roleName).ConfigureAwait(false);
            var mappingResult = _mapper.Map<List<ClaimTreeViewItem>>(result.Result);
            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Get users in role.
        /// </summary>
        /// <param name="roleName">Role name.</param>
        /// <returns>Returns list of users in the role. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Return role user info.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("role-user-info/{roleName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleUserList)]
        [ProducesResponseType(typeof(ServiceResponse<List<UserRoleInfoResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleGetRoleUserInfoResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetRoleUserInfoAsync(string roleName)
        {
            var result = await _roleService.GetRoleUserInfoAsync(roleName).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var usersInRoleResponse = _mapper.Map<List<UserRoleInfoDto>, List<UserRoleInfoResponse>>(result.Result);
            return Ok(Success(usersInRoleResponse));
        }

        /// <summary>
        /// Get users by role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <returns>Returns RoleUserResponseModel.Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{roleName}/users")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleUserList)]
        [ProducesResponseType(typeof(ServiceResponse<List<UserInRoleResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleGetUsersInRoleResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetUsersInRoleAsync(string roleName)
        {
            var result = await _roleService.GetUsersInRoleAsync(roleName).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var users = _mapper.Map<List<UserInRoleResponse>>(result.Result);

            return Ok(Success(users));
        }

        /// <summary>
        /// Create a application role.
        /// </summary>
        /// <param name="role">RolePostRequest Model</param>
        /// <returns>Returns http status codes(200,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleCreate)]
        [SwaggerRequestExample(typeof(RolePostRequest), typeof(RolePostRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<CreatedAtRouteResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RolePosteResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PostAsync([FromBody] RolePostRequest role)
        {
            var mappingModel = _mapper.Map<ApplicationRoleDto>(role);
            var result = await _roleService.PostAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Update application role partially.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="role">RolePutRequest Model</param>
        /// <returns>Returns ApplicationRole item. Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut("{roleName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleEdit)]
        [SwaggerRequestExample(typeof(RolePutRequest), typeof(RolePutRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<ApplicationRole>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RolePutResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutAsync(string roleName, [FromBody] RolePutRequest role)
        {
            var mappingModel = _mapper.Map<RolePutRequest, RoleUpdateDto>(role);
            mappingModel.RoleName = roleName;

            var result = await _roleService.PutAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            var mappingResult = _mapper.Map<ApplicationRoleDto, RolePutResponse>(result.Result);

            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Remove claim from role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="claimValue">Claim Value</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{roleName}/claims/{claimvalue}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleRemoveClaim)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> RemoveClaimFromRoleAsync(string roleName, string claimValue)
        {
            var result = await _roleService.RemoveClaimFromRoleAsync(roleName, claimValue).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Remove user from role.
        /// </summary>
        /// <param name="roleName">Role Name</param>
        /// <param name="username">Username</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{roleName}/users/{username}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleRemoveUser)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> RemoveUserFromRoleAsync(string roleName, string username)
        {
            var result = await _roleService.RemoveUserFromRoleAsync(roleName, username).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Save role claims.
        /// </summary>
        /// <param name="model">SaveRoleClaimsModel Model</param>
        /// <returns>Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("role-claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleAddClaim)]
        [SwaggerRequestExample(typeof(SaveRoleClaimsModel), typeof(RoleSaveRoleClaimsRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SaveRoleClaimsAsync([FromBody] SaveRoleClaimsModel model)
        {
            var mappingModel = _mapper.Map<SaveRoleClaimsDto>(model);
            var result = await _roleService.SaveRoleClaimsAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Search a role by role name.
        /// </summary>
        /// <param name="roleGetRequest">Request object for fetching roles with paging properties.</param>
        /// <returns>Returns found ApplicationRole list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementRoleList)]
        [SwaggerRequestExample(typeof(RoleSearchRequest), typeof(RoleSearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<RoleGetResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RoleSearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] RoleSearchRequest roleGetRequest)
        {
            var mappingModel = _mapper.Map<RoleSearchDto>(roleGetRequest);
            var result = await _roleService.SearchAsync(mappingModel).ConfigureAwait(false);
            var roleGetResponse = _mapper.Map<PagedResultDto<ApplicationRoleDto>, PagedResult<RoleGetResponse>>(result.Result);
            return Ok(Success(roleGetResponse));
        }
    }
}