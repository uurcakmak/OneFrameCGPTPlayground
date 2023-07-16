// <copyright file="AuthenticationController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using AutoMapper;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneFrameCGPTPlayground.Application.Abstractions.Account;
using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.WebAPI.Model.Authentication;
using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;

namespace OneFrameCGPTPlayground.WebAPI.Controllers
{
    [Route("authentications")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Send verification code.
        /// </summary>
        /// <param name="twoFactorVerificationRequest">TwoFactorVerificationRequest Model</param>
        /// <returns>Returns return resetUrl. Returns http status codes(200,204,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="204">Item(s) was not found. (No content).</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("send-verification-code")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(TwoFactorVerificationRequest), typeof(TwoFactorVerificationRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponseStringExample))]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status204NoContent, typeof(ServiceResponse204Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> SendVerificationCodeAsync([FromBody] TwoFactorVerificationRequest twoFactorVerificationRequest)
        {
            var mappingModel = _mapper.Map<TwoFactorVerificationDto>(twoFactorVerificationRequest);
            var response = await _authenticationService.SendVerificationCodeAsync(mappingModel).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Two factor verification.
        /// </summary>
        /// <param name="twoFactorVerificationRequest">TwoFactorVerificationRequest Model</param>
        /// <returns>Returns return resetUrl. Returns http status codes(200,400,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="400">Bad request. The server could not understand the request because of invalid syntax.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("two-factor-verification")]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(TwoFactorVerificationRequest), typeof(TwoFactorVerificationRequestExample))]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ServiceResponse200Example))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ServiceResponse400Example))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> TwoFactorVerificationAsync([FromBody] TwoFactorVerificationRequest twoFactorVerificationRequest)
        {
            var mappingModel = _mapper.Map<TwoFactorVerificationDto>(twoFactorVerificationRequest);
            var response = await _authenticationService.TwoFactorVerificationAsync(mappingModel).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            return Ok(Success(response));
        }

        /// <summary>
        /// Generate authenticator shared key for 2FA with authenticator only.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>Returns http status codes(200,500).</returns>
        /// <response code="200">Request ended completely successful.</response>
        /// <response code="500">An error occurred while processing your request. The server has encountered a situation that it does not know how to handle.</response>
        [HttpPost("generate-authenticator-shared-key")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ServiceResponse<AuthenticatorResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AuthenticationGenerateAuthenticatorSharedKeyResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ServiceResponse500Example))]
        public async Task<IActionResult> GenerateAuthenticatorSharedKeyAsync([FromBody] string userName)
        {
            var response = await _authenticationService.GenerateAuthenticatorSharedKeyAsync(userName).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return BadRequest(response);
            }

            var loginResponse = _mapper.Map<AuthenticatorResponseDto, AuthenticatorResponse>(response.Result);
            return Ok(Success(loginResponse));
        }
    }
}