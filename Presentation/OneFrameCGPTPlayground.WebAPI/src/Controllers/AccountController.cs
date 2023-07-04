// <copyright file="AccountController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using OneFrameCGPTPlayground.Application.Abstractions.Account;
using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Notification;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using OneFrameCGPTPlayground.WebAPI.Model.Paging;
using OneFrameCGPTPlayground.WebAPI.Model.Profile;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Globalization;
using System.Net.Mime;

using UserClaimPostRequest = OneFrameCGPTPlayground.WebAPI.Model.User.UserClaimPostRequest;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("accounts")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IApplicationSettingService _applicationSettingService;
        private readonly IKsStringLocalizer<AccountController> _localize;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IEmailNotificationService emailNotificationService, IKsI18N i18N, IMapper mapper, IApplicationSettingService applicationSettingService, IConfiguration configuration)
        {
            _accountService = accountService;
            _emailNotificationService = emailNotificationService;
            _localize = i18N.GetLocalizer<AccountController>();
            _mapper = mapper;
            _applicationSettingService = applicationSettingService;
            _configuration = configuration;
        }

        /// <summary>
        /// Add claim to user by UserClaimPostRequest model.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="model">UserClaimPostRequest Model</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("{username}/claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserAddClaim)]
        [SwaggerRequestExample(typeof(UserClaimPostRequest), typeof(AccountAddClaimToUserRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> AddClaimToUserAsync(string username, [FromBody] UserClaimPostRequest model)
        {
            var response = await _accountService.AddClaimToUserAsync(username, model.Name).ConfigureAwait(false);

            if (!response.IsSuccessful && response.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Change user password by ChangePasswordRequest model.
        /// </summary>
        /// <param name="changePasswordRequest">ChangePasswordRequest Model</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut("change-password")]
        [Authorize]
        [SwaggerRequestExample(typeof(ChangePasswordRequest), typeof(AccountChangePasswordRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var response = await _accountService.ChangePasswordAsync(User.Identity.GetUsername(), changePasswordRequest.CurrentPassword, changePasswordRequest.NewPassword).ConfigureAwait(false);

            if (!response.IsSuccessful && response.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Change user expired password by ChangePasswordExpiredRequest model.
        /// </summary>
        /// <param name="requestModel">ChangePasswordExpiredRequest Model</param>
        /// <returns>Returns http status codes(200,204,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut("change-password-expired")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(ChangePasswordExpiredRequest), typeof(AccountChangePasswordExpiredRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> ChangePasswordExpiredAsync([FromBody] ChangePasswordExpiredRequest requestModel)
        {
            var response = await _accountService.ChangePasswordExpiredAsync(requestModel.UserName, requestModel.CurrentPassword, requestModel.NewPassword).ConfigureAwait(false);

            if (!response.IsSuccessful && response.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{username}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserDelete)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> DeleteAsync(string username)
        {
            var response = await _accountService.DeleteUserAsync(username, User.Identity.GetUsername()).ConfigureAwait(false);

            if (!response.IsSuccessful && response.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="model">ForgotPasswordRequest Model</param>
        /// <returns>Returns http status codes(200,204,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(ForgotPasswordRequest), typeof(AccountForgotPasswordRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest model)
        {
            var response = await _accountService.SendResetPasswordMailAsync(model.Email, Request.Scheme).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <param name="pagedRequest">Request object for fetching users with paging properties.</param>
        /// <returns>Returns found ApplicationUser list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("list")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        [SwaggerRequestExample(typeof(PagedRequest), typeof(PagedRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<UserGetResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountGetResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync([FromQuery] PagedRequest pagedRequest)
        {
            var mappingRequest = _mapper.Map<PagedRequestDto>(pagedRequest);
            var result = await _accountService.GetUserListAsync(mappingRequest).ConfigureAwait(false);
            var mappingResult = _mapper.Map<PagedResultDto<UserDto>, PagedResult<UserGetResponse>>(result.Result);

            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Get a user by username.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Returns found ApplicationUser. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{username}", Name = "UserGet")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        [ProducesResponseType(typeof(ServiceResponse<UserGetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountGetWithUsernameResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetAsync(string username)
        {
            var result = await _accountService.GetUserByUsernameAsync(username).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var mappingResult = _mapper.Map<UserDto, UserGetResponse>(result.Result);
            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Get claim(s) in user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Returns claim list. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("{username}/claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserClaimList)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClaimResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountGetClaimsInUserResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetClaimsInUserAsync(string username)
        {
            var result = await _accountService.GetClaimsInUserAsync(username).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var mappingResult = _mapper.Map<List<ClaimDto>, List<ClaimResponse>>(result.Result);

            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Get role assignments in user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Returns role assignment list. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("role-assignments/{username}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        [ProducesResponseType(typeof(ServiceResponse<List<RoleAssignmentResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountGetRoleAssignmentsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetRoleAssignmentsAsync(string username)
        {
            var result = await _accountService.GetRoleAssignmentsAsync(username).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return Ok(result);
            }

            var mappingResult = _mapper.Map<List<RoleAssignmentDto>, List<RoleAssignmentResponse>>(result.Result);
            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Get tree of claims in user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Returns claim tree. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("user-claims-tree/{userName}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserClaimList)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClaimTreeViewItem>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountGetUserClaimsTreeResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetUserClaimsTreeAsync(string username)
        {
            var result = await _accountService.GetUserClaimsTreeAsync(username).ConfigureAwait(false);
            var mappingResult = _mapper.Map<List<ClaimTreeViewItemDto>, List<ClaimTreeViewItem>>(result.Result);
            return Ok(Success(mappingResult));
        }

        /// <summary>
        /// Captcha validation.
        /// </summary>
        /// <param name="captcha">Captcha Token</param>
        /// <returns>Returns true/false. Returns http status codes(200,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("is-captcha-passed")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponseBooleanExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> IsCaptchaPassedAsync(string captcha)
        {
            var result = await _accountService.IsCaptchaPassedAsync(captcha).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="model">LoginRequest Model</param>
        /// <returns>Returns http status codes(200,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(LoginRequest), typeof(AccountLoginRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountLoginResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest model)
        {
            var mappingDto = _mapper.Map<LoginRequestDto>(model);
            var result = await _accountService.LoginAsync(mappingDto).ConfigureAwait(false);

            if (result.IsSuccessful)
            {
                var loginResponse = _mapper.Map<LoginDto, LoginResponse>(result.Result);
                return Ok(Success(loginResponse));
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="user">UserPostRequest Model</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserCreate)]
        [SwaggerRequestExample(typeof(UserPostRequest), typeof(AccountPostRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<CreatedAtRouteResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountPostResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PostAsync([FromBody] UserPostRequest user)
        {
            var mappingDto = _mapper.Map<UserDto>(user);
            var createdUser = await _accountService.PostAsync(mappingDto, Request.Scheme, User.Identity.GetUsername()).ConfigureAwait(false);

            if (!createdUser.IsSuccessful)
            {
                return BadRequest(createdUser);
            }

            var userGetResponse = _mapper.Map<UserGetResponse>(createdUser.Result);
            return Ok(Success(userGetResponse));
        }

        /// <summary>
        /// Update application role partially.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="user">UserPutRequest Model</param>
        /// <returns>Returns ApplicationUser item. Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut("{username}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserEdit)]
        [SwaggerRequestExample(typeof(UserPutRequest), typeof(AccountPutRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<UserGetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountPutResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutAsync(string username, [FromBody] UserPutRequest user)
        {
            var mappingModel = _mapper.Map<UserUpdateDto>(user);
            var result = await _accountService.PutAsync(username, mappingModel, User.Identity.GetUsername()).ConfigureAwait(false);

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

            var userResponse = _mapper.Map<UserDto, UserGetResponse>(result.Result);

            return Ok(Success(userResponse));
        }

        /// <summary>
        /// Update application role partially.
        /// </summary>
        /// <param name="userRole">UserRolePutRequest model.</param>
        /// <returns>Returns ApplicationUser item. Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut("user-role")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserRole)]
        [SwaggerRequestExample(typeof(UserRolePutRequest), typeof(AccountRolePutRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountPutResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutForUserRoleAsync([FromBody] UserRolePutRequest userRole)
        {
            var mappingModel = _mapper.Map<UserRoleUpdateDto>(userRole);
            var result = await _accountService.PutForUserRoleAsync(userRole.Username, mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Update application role partially.
        /// </summary>
        /// <param name="userPutForUserRequest">UserPutForUserProfileRequest Model</param>
        /// <returns>Returns ApplicationUser item. Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPut("change-profile-info")]
        [Authorize]
        [SwaggerRequestExample(typeof(UserPutForUserProfileRequest), typeof(AccountPutForUserRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<UserGetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountPutForUserResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> PutForUserAsync([FromBody] UserPutForUserProfileRequest userPutForUserRequest)
        {
            var mappingModel = _mapper.Map<UserUpdateDto>(userPutForUserRequest);

            var result = await _accountService.PutForUserProfileAsync(User.Identity.GetUsername(), mappingModel).ConfigureAwait(false);

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

            var userResponse = _mapper.Map<UserDto, UserGetResponse>(result.Result);

            return Ok(Success(userResponse));
        }

        /// <summary>
        /// Refresh Token.
        /// </summary>
        /// <param name="request">RefreshRequest Model</param>
        /// <returns>Returns LoginResponse Model. Returns http status codes(200,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(RefreshRequest), typeof(AccountRefreshRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountRefreshResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public IActionResult Refresh([FromBody] RefreshRequest request)
        {
            var result = _accountService.Refresh(request.RefreshToken);

            if (result.IsSuccessful)
            {
                var data = _mapper.Map<LoginResponse>(result.Result);
                return Ok(Success(data));
            }

            return BadRequest(result);
        }

        /// <summary>
        /// User registration.
        /// </summary>
        /// <param name="model">UserRegisterRequest Model</param>
        /// <returns>Returns LoginResponse model. Returns http status codes(200,204,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("register")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(UserRegisterRequest), typeof(AccountRegisterRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountRegisterResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest model)
        {
            var mappingDto = _mapper.Map<UserDto>(model);
            var result = await _accountService.RegisterAsync(mappingDto).ConfigureAwait(false);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            var activationUrlResponse = await _accountService.CreateEmailActivationUrlAsync(model.Email).ConfigureAwait(false);

            if (!activationUrlResponse.IsSuccessful)
            {
                return BadRequest(activationUrlResponse);
            }

            var activationTemplateResponse = await _accountService.GetEmailActivationTemplateAsync(activationUrlResponse.Result).ConfigureAwait(false);

            if (!activationTemplateResponse.IsSuccessful)
            {
                return BadRequest(activationTemplateResponse);
            }

            var activationTemplate = activationTemplateResponse.Result;
            await _emailNotificationService.SendEmailAsync(activationTemplate.Subject, activationTemplate.EmailContent, model.Email).ConfigureAwait(false);
            var data = _mapper.Map<LoginResponse>(result.Result);

            return Ok(Success(data));
        }

        /// <summary>
        /// Remove claim from user.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="claimValue">Claim Value</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("{username}/claims/{claimValue}")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserRemoveClaim)]
        [ProducesResponseType(typeof(ServiceResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponseBooleanExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> RemoveClaimFromUserAsync(string username, string claimValue)
        {
            var result = await _accountService.RemoveClaimFromUserAsync(username, claimValue).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="model">ResetPasswordRequest Model</param>
        /// <returns>Returns http status codes(200,204,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(ResetPasswordRequest), typeof(AccountResetPasswordRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponseStringExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest model)
        {
            var mappingModel = _mapper.Map<ResetPasswordDto>(model);
            var result = await _accountService.ResetPasswordAsync(mappingModel).ConfigureAwait(false);

            if (!result.IsSuccessful && result.Error.Code == StatusCodes.Status400BadRequest)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Add claim to user.
        /// </summary>
        /// <param name="model">SaveUserClaimsModel model.</param>
        /// <returns>Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("user-claims")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserAddClaim)]
        [SwaggerRequestExample(typeof(SaveUserClaimsModel), typeof(AccountSaveUserClaimsRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SaveUserClaimsAsync([FromBody] SaveUserClaimsModel model)
        {
            var mappingModel = _mapper.Map<SaveUserClaimsDto>(model);
            var result = await _accountService.SaveUserClaimsAsync(mappingModel).ConfigureAwait(false);

            return Ok(result);
        }

        /// <summary>
        /// Search a user by username.
        /// </summary>
        /// <param name="userGetRequest">Request object for fetching users with paging properties.</param>
        /// <returns>Returns found ApplicationUser list. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("search")]
        [Authorize(Policy = KsPermissionPolicy.ManagementUserList)]
        [SwaggerRequestExample(typeof(UserSearchRequest), typeof(AccountSearchRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse<PagedResult<UserGetResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AccountSearchResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SearchAsync([FromQuery] UserSearchRequest userGetRequest)
        {
            var mappingResult = _mapper.Map<UserSearchDto>(userGetRequest);
            var result = await _accountService.SearchAsync(mappingResult).ConfigureAwait(false);

            var userGetResponse = _mapper.Map<PagedResultDto<UserDto>, PagedResult<UserGetResponse>>(result.Result);
            return Ok(Success(userGetResponse));
        }

        /// <summary>
        /// Set profile photo as base64 string.
        /// </summary>
        /// <param name="model">ProfilePhotoModel Model</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("set-profile-photo")]
        [Authorize]
        [SwaggerRequestExample(typeof(ProfilePhotoModel), typeof(AccountSetProfilePhotoRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SetProfilePhotoAsync([FromBody] ProfilePhotoModel model)
        {
            if (string.IsNullOrEmpty(model.Photo))
            {
                return await Task.Run(() => BadRequest(Error(_localize["InvalidModel"], StatusCodes.Status400BadRequest))).ConfigureAwait(false);
            }

            string[] baseDataImageInformations = model.Photo.Split(',');
            byte[] bytes = Convert.FromBase64String(baseDataImageInformations[1]);

            var profilePhotoSizeResponse = await _applicationSettingService.GetByKeyAsync(ConfigurationConstant.IdentityProfilePhotoMaxSize).ConfigureAwait(false);
            int profilePhotoSize = 0;
            if (profilePhotoSizeResponse.IsSuccessful)
            {
                profilePhotoSize = Convert.ToInt32(profilePhotoSizeResponse.Result.Value);
            }

            if (bytes.Length > profilePhotoSize)
            {
                return await Task.Run(() => BadRequest(Error(_localize["InvalidProfilePhotoSize"], StatusCodes.Status400BadRequest))).ConfigureAwait(false);
            }

            var response = _accountService.SetProfilePhotoAsync(User.Identity.GetUsername(), model.Photo);
            return Ok(response.Result);
        }

        /// <summary>
        /// Delete profile photo of user.
        /// </summary>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpDelete("delete-profile-photo")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> DeleteProfilePhotoAsync()
        {
            var response = await _accountService.DeleteProfilePhotoAsync(User.Identity.GetUsername()).ConfigureAwait(false);
            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Confirm email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="token">Token</param>
        /// <returns>Returns http status codes(200,204,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> ConfirmEmailAsync(string email, string token)
        {
            var confirmationResult = await _accountService.ConfirmEmailAsync(email, token).ConfigureAwait(false);

            if (!confirmationResult.IsSuccessful)
            {
                return Ok(confirmationResult);
            }

            return Ok(Success());
        }

        /// <summary>
        /// External login.
        /// </summary>
        /// <param name="provider">Provider</param>
        /// <param name="returnUrl">Return Url</param>
        /// <returns>Returns http status codes(400,401,500).</returns>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("external-login/{provider}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public IActionResult ExternalLogin(string provider, [FromQuery] string returnUrl)
        {
            var redirectUrl = _configuration["SiteInformations:HostAddress"] + "accounts/external-login-callback" + "?returnUrl=" + returnUrl;

            var propertiesResponse = _accountService.GetAuthenticationProperties(provider, redirectUrl);
            var properties = propertiesResponse.Result;
            properties.AllowRefresh = true;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(provider) switch
            {
                GoogleDefaults.AuthenticationScheme => Challenge(properties, GoogleDefaults.AuthenticationScheme),
                FacebookDefaults.AuthenticationScheme => Challenge(properties, FacebookDefaults.AuthenticationScheme),
                MicrosoftAccountDefaults.AuthenticationScheme => Challenge(properties, MicrosoftAccountDefaults.AuthenticationScheme),
                _ => BadRequest(),
            };
        }

        [HttpGet("external-login-callback")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallbackAsync(string returnUrl = null)
        {
            var result = await _accountService.ExternalLoginAsync().ConfigureAwait(false);

            return Redirect(returnUrl + "?token=" + result.Result.Token + "&refreshToken=" + result.Result.RefreshToken);
        }
    }
}