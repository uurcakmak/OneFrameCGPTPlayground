// <copyright file="UserController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFrameCGPTPlayground.Application.Abstractions.User;
using OneFrameCGPTPlayground.Application.Abstractions.UserConfirmationHistory.Contract;
using OneFrameCGPTPlayground.Common.Helpers;
using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("users")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UserController : BaseController
    {
        private readonly IClaimManager _claimManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper, IClaimManager claimManager)
        {
            _userService = userService;
            _mapper = mapper;
            _claimManager = claimManager;
        }

        /// <summary>
        /// Gets the user claims.
        /// </summary>
        /// <returns>Returns list of user claims. Returns http status codes(200,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("claims")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<List<ClaimResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserGetUserClaimsResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public IActionResult GetUserClaims()
        {
            var claims = _claimManager.GetClaims();

            var result = claims.Select(claim => new ClaimResponse { Value = claim.Value, Name = claim.Type }).ToList();

            return Ok(Success(result));
        }

        /// <summary>
        /// Get user info.
        /// </summary>
        /// <returns>Returns user info. Returns http status codes(200,204,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("user-info")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<BasicUserInfoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserGetUserInfoAsyncResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetUserInfoAsync()
        {
            var result = await _userService.GetCurrentUserInfoAsync(User.Identity.GetUsername()).ConfigureAwait(false);

            if (result.IsSuccessful)
            {
                var model = _mapper.Map<BasicUserInfoResponse>(result.Result);

                return Ok(Success(model));
            }

            return Ok(result);
        }

        /// <summary>
        /// Get confirmation code.
        /// </summary>
        /// <param name="phoneNumber">Phone Number</param>
        /// <returns>Returns confirmation code. Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpGet("user-confirmation-code")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse<UserGetConfirmationCodeResponseExample>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UserGetConfirmationCodeResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GetConfirmationCodeAsync(string phoneNumber)
        {
            var codeResponse = await _userService.GetConfirmationCodeAsync(User.Identity.GetUsername(), phoneNumber).ConfigureAwait(false);

            if (!codeResponse.IsSuccessful)
            {
                return Ok(codeResponse);
            }

            var model = _mapper.Map<UserConfirmationHistoryDto, ConfirmationCodeResponse>(codeResponse.Result);

            return Ok(Success(model));
        }

        /// <summary>
        /// Send code.
        /// </summary>
        /// <param name="confirmationHistoryDto">UserConfirmationHistoryDto Model</param>
        /// <returns>Returns http status codes(200,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("user-send-code")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SendCodeAsync([FromBody] UserConfirmationHistoryDto confirmationHistoryDto)
        {
            var sendingResponse = await _userService.SendConfirmationCodeAsync(confirmationHistoryDto).ConfigureAwait(false);

            if (!sendingResponse.IsSuccessful)
            {
                return Ok(sendingResponse);
            }

            var checkingResponse = await _userService.CheckConfirmationCodeAsync(confirmationHistoryDto).ConfigureAwait(false);

            if (!checkingResponse.IsSuccessful)
            {
                return Ok(checkingResponse);
            }

            return Ok(Success());
        }

        /// <summary>
        /// Confirmation code.
        /// </summary>
        /// <param name="request">ConfirmationCodeRequest Model</param>
        /// <returns>Returns http status codes(200,204,400,401,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="401">Unauthorized request. The request has not been applied because the server requires user authentication.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("user-confirmation-code")]
        [Authorize]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse))]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> ConfirmCodeAsync([FromBody] ConfirmationCodeRequest request)
        {
            var mappingDto = _mapper.Map<ConfirmationCodeRequest, UserConfirmationHistoryDto>(request);

            var confirmationResponse = await _userService.ConfirmCodeAsync(mappingDto).ConfigureAwait(false);

            if (!confirmationResponse.IsSuccessful)
            {
                return Ok(confirmationResponse);
            }

            var checkReponse = await _userService.CheckUserConfirmationAsync(User.Identity.GetUsername()).ConfigureAwait(false);

            if (!checkReponse.IsSuccessful)
            {
                return Ok(checkReponse);
            }

            return Ok(Success(confirmationResponse));
        }
    }
}
