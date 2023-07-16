// <copyright file="AccountController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using IdentityModel;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Captcha;
using OneFrameCGPTPlayground.Mvc.Extensions;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.Account;
using System.IdentityModel.Tokens.Jwt;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("accounts")]
    public class AccountController : BaseController<AccountController>
    {
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IConfiguration _configuration;
        private readonly IKsStringLocalizer<AccountController> _localize;
        private readonly ILogger _logger;
        private readonly IClaimHelper _claimHelper;

        public AccountController(ILogger<AccountController> logger, IKsI18N i18N, ICaptchaValidator captchaValidator, IConfiguration configuration, IClaimHelper claimHelper)
            : base(i18N)
        {
            _logger = logger;
            _localize = i18N.GetLocalizer<AccountController>();
            _captchaValidator = captchaValidator;
            _configuration = configuration;
            _claimHelper = claimHelper;
        }

        [HttpGet("access-denied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet("check-email")]
        [AllowAnonymous]
        public IActionResult CheckEmail(string token, string email, bool newUser)
        {
            var model = new ResetPasswordViewModel
            {
                Token = token?.Replace(" ", "+"),
                Email = email,
            };

            if (newUser)
            {
                model.Title = _localize["ActivateUser"];
                model.ActionButtonLabel = _localize["Save"];
            }
            else
            {
                model.Title = _localize["ResetPassword"];
                model.ActionButtonLabel = _localize["Reset"];
            }

            return View("ResetPassword", model);
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string token, [FromQuery] string email)
        {
            var response = await GetApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountConfirmEmail, new { email, token }).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                if (response.Error.Message == _localize["EmailAlreadyConfirmed"])
                {
                    return View("EmailAlreadyConfirmed");
                }

                return ToastError(_localize["EmailConfirmationFailed"]);
            }

            return View("EmailConfirmed");
        }

        [HttpGet("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountForgotPassword, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error.Message ?? _localize["EmailResetLinkFailed"]);
            }

            return Ok(response);
        }

        [HttpGet("password-email-sent")]
        [AllowAnonymous]
        public IActionResult PasswordEmailSent()
        {
            return View();
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme).ConfigureAwait(false);
                ViewData["ReturnUrl"] = returnUrl;
                var model = new LoginViewModel();
                model.Email = _configuration["DefaultLoginCredentials:username"];
                model.Password = _configuration["DefaultLoginCredentials:password"];

                return View(model);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            _ = bool.TryParse(_configuration["ReCaptcha:IsEnabled"], out var reCaptchaIsEnabled);

            if (reCaptchaIsEnabled)
            {
                var captchaResult = await _captchaValidator.IsCaptchaPassedAsync(model.Captcha).ConfigureAwait(false);

                if (!captchaResult)
                {
                    return ToastError(_localize["CaptchaValidation"]);
                }
            }

            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            var response = await PostApiRequestWithAllHeadersAsync<ServiceResponse<LoginResponseViewModel>>(ApiEndpoints.AccountLogin, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                if (response.Result != null && response.Result.PasswordExpired)
                {
                    return ToastSuccessForRedirect(_localize["PasswordExpired"], $"{MvcEndpoints.AccountPasswordExpired}/{model.Email}");
                }

                return ToastError(response.Error);
            }

            var dbConfiguration = GetConfigurationsAsync(new List<string>
            {
                ConfigurationConstant.Identity2FaSettingsIsEnabled,
            }).Result;

            if (dbConfiguration.Identity2FaSettingsIsEnabled)
            {
                TempData.Put("LoginModel", model);

                return ToastSuccessForRedirect(_localize["LoginSuccess"], Url.Action("Login2FA", "Authentication"));
            }

            await _claimHelper.BuildClaimsAndSignIn(response.Result).ConfigureAwait(false);

            SetTimeZoneAndFullNameCookie(response.Result.Token);

            return ToastSuccessForRedirect(_localize["LoginSuccess"], !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : Url.Action("Index", "Home"));
        }

        [HttpGet("external-login/{provider}")]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnViewUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(returnViewUrl))
                {
                    return Redirect(returnViewUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            var externalLogin = new ExternalLoginViewModel
            {
                Provider = provider.ToUpperInvariant(),
                ReturnUrl = string.IsNullOrEmpty(returnViewUrl) ? "/" : returnViewUrl
            };

            TempData.Put("ExternalLoginModel", externalLogin);

            var apiUrl = _configuration["Identity:Jwt:IssuerSettings:BaseAddress"] + _configuration["Identity:Jwt:IssuerSettings:ExternalLogin"] + provider;
            var clientReturnUrl = _configuration["SiteInformations:HostAddress"] + "accounts/external-login-callback";

            var redirectUrl = apiUrl + "?returnUrl=" + clientReturnUrl;

            return Redirect(redirectUrl);
        }

        [HttpGet("external-login-callback")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallbackAsync(string token, string refreshToken)
        {
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            await _claimHelper.BuildClaimsAndSignIn(new LoginResponseViewModel { Token = token, RefreshToken = refreshToken }).ConfigureAwait(false);

            SetTimeZoneAndFullNameCookie(token);

            ExternalLoginViewModel externalLoginViewModel = TempData.Get<ExternalLoginViewModel>("ExternalLoginModel");
            return View(externalLoginViewModel);
        }

        [HttpGet("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> LogoutAsync()
        {
            _logger.LogInformation($"User {User.Identity.Name} logged out at {DateTime.UtcNow}.");

            HttpContext.Response.Cookies.Delete(JwtBearerDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme).ConfigureAwait(false);
            return RedirectToAction("Login");
        }

        [HttpGet("password-expired/{username}")]
        [AllowAnonymous]
        public IActionResult PasswordExpired(string username)
        {
            var model = new PasswordExpiredViewModel { UserName = username };
            return View(model);
        }

        [HttpPost("password-expired-change")]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordExpiredChangeAsync(PasswordExpiredViewModel model)
        {
            if (model.CurrentPassword.Equals(model.NewPassword, StringComparison.CurrentCulture))
            {
                return ToastError(_localize["NewPasswordCanNotBeSameWithTheCurrentOne"]);
            }

            if (!model.NewPassword.Equals(model.NewPasswordConfirmation, StringComparison.CurrentCulture))
            {
                return ToastError(_localize["PasswordsDoesNotMatch"]);
            }

            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountChangePasswordExpired, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["ChangePasswordSuccess"], "/");
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (!registerViewModel.Password.Equals(registerViewModel.ConfirmPassword, StringComparison.CurrentCulture))
            {
                return ToastError(_localize["PasswordsDoesNotMatch"]);
            }

            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountRegister, registerViewModel).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response);
        }

        [HttpGet("register-email-sent")]
        [AllowAnonymous]
        public IActionResult RegisterEmailSent()
        {
            return View();
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountResetPassword, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response);
        }

        [HttpGet("password-changed")]
        [AllowAnonymous]
        public IActionResult PasswordChanged()
        {
            return View();
        }

        private void SetTimeZoneAndFullNameCookie(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var zoneInfoClaim = securityToken.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.ZoneInfo);
            var nameClaim = securityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.GivenName);
            var surnameClaim = securityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.FamilyName);

            CookieHelper.Write(Response, CookieHelper.FullName, nameClaim.Value + ' ' + surnameClaim.Value, _configuration["CookieSettings:DefaultExpireDays"]);
            CookieHelper.Write(Response, CookieHelper.TimeZone, zoneInfoClaim.Value, _configuration["CookieSettings:TimeZoneExpireDays"]);
        }
    }
}