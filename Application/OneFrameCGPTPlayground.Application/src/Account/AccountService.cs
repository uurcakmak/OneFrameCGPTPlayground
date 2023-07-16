// <copyright file="AccountService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using IdentityModel;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OneFrameCGPTPlayground.Application.Abstractions;
using OneFrameCGPTPlayground.Application.Abstractions.Account;
using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplate;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.LoginAuditLog.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Notification;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory;
using OneFrameCGPTPlayground.Application.Abstractions.UserPasswordHistory.Contracts;
using OneFrameCGPTPlayground.Application.Helpers;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Common.Enums;
using OneFrameCGPTPlayground.Common.Helpers;
using OneFrameCGPTPlayground.Domain;
using OneFrameCGPTPlayground.Infrastructure.Helpers;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Captcha;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Account
{
    /// <summary>
    /// AccountService.
    /// </summary>
    /// <seealso cref="IAccountService" />
    public class AccountService : ApplicationServiceBase<AccountService>, IAccountService
    {
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IClaimManager _claimManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILoginAuditLogService _loginAuditLogService;
        private readonly PasswordOptions _passwordOptions;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserPasswordHistoryService _userPasswordHistoryService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IKsStringLocalizer<AccountService> _localize;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="emailNotificationService">The email notification service.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="claimManager">The claim manager.</param>
        /// <param name="tokenHelper">The token helper.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="keyNormalizer">The key normalizer.</param>
        /// <param name="identityOptions">IdentityOptions parameter.</param>,
        /// <param name="userPasswordHistoryService">userPasswordHistoryService parameter.</param>
        /// <param name="captchaValidator">captchaValidator parameter.</param>
        /// <param name="loginAuditLogService">loginAuditLogService parameter.</param>
        /// <param name="httpContextAccessor">httpContextAccessor parameter.</param>
        public AccountService(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailNotificationService emailNotificationService, RoleManager<ApplicationRole> roleManager, IClaimManager claimManager, ITokenHelper tokenHelper, SignInManager<ApplicationUser> signInManager, ILookupNormalizer keyNormalizer, IOptions<IdentityOptions> identityOptions, IUserPasswordHistoryService userPasswordHistoryService, ICaptchaValidator captchaValidator, ILoginAuditLogService loginAuditLogService, IHttpContextAccessor httpContextAccessor, IEmailTemplateService emailTemplateService, IKsI18N i18N)
            : base(serviceProvider)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailNotificationService = emailNotificationService;
            _roleManager = roleManager;
            _claimManager = claimManager;
            _tokenHelper = tokenHelper;
            _signInManager = signInManager;
            _keyNormalizer = keyNormalizer;
            _passwordOptions = identityOptions.Value.Password;
            _userPasswordHistoryService = userPasswordHistoryService;
            _captchaValidator = captchaValidator;
            _loginAuditLogService = loginAuditLogService;
            _httpContextAccessor = httpContextAccessor;
            _emailTemplateService = emailTemplateService;
            _localize = i18N.GetLocalizer<AccountService>();
        }

        /// <summary>
        /// Adds the claim to user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="claim">The claim.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> AddClaimToUserAsync(string username, string claim)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var userClaims = await GetClaimsInUserAsync(username).ConfigureAwait(false);

            if (userClaims.Result.Exists(x => x.Name == claim))
            {
                return ServiceResponseHelper.SetError(Localize["ClaimAlreadyExist", claim], StatusCodes.Status204NoContent, true);
            }

            var result = await _userManager.AddClaimAsync(user, new Claim("userClaim", claim)).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (!_userPasswordHistoryService.PasswordHistoryValidation(user, newPassword))
            {
                return ServiceResponseHelper.SetError(Localize["LastPasswordUsingError", _configuration["Identity:Policy:Password:HistoryLimit"]], StatusCodes.Status400BadRequest, true);
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword).ConfigureAwait(false);

            _ = await _userManager.UpdateAsync(user).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            _ = await _userPasswordHistoryService.CreateAsync(new UserPasswordHistoryDto
            {
                PasswordHash = user.PasswordHash,
                UserId = user.Id,
            }).ConfigureAwait(false);

            user.LastPasswordChangedDate = DateTime.UtcNow;
            var resultUpdate = await _userManager.UpdateAsync(user).ConfigureAwait(false);

            if (!resultUpdate.Succeeded)
            {
                return ServiceResponseHelper.SetError(resultUpdate.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Changes the expired password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Documentation", "CA1200:Avoid using cref tags with a prefix", Justification = "warning")]
        public async Task<ServiceResponse> ChangePasswordExpiredAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            _ = int.TryParse(_configuration["Identity:Policy:Password:ExpireDays"], out var passwordExpireDays);

            if (user.LastPasswordChangedDate.AddDays(passwordExpireDays) < DateTime.UtcNow)
            {
                return await ChangePasswordAsync(username, currentPassword, newPassword).ConfigureAwait(false);
            }

            return ServiceResponseHelper.SetError(Localize["UnableToResetPassword"], StatusCodes.Status400BadRequest, true);
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> DeleteUserAsync(string username, string updatedUserName)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            user.UpdatedUser = updatedUserName;
            user.UpdatedDate = DateTime.UtcNow;
            user.IsDeleted = true;
            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Gets the claims in user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<ClaimDto>>> GetClaimsInUserAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError<List<ClaimDto>>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var claims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);

            var response = new List<ClaimDto>();

            if (claims != null && claims.Count > 0)
            {
                foreach (var claim in claims)
                {
                    response.Add(new ClaimDto()
                    {
                        Name = claim.Value,
                    });
                }
            }

            return ServiceResponseHelper.SetSuccess(response);
        }

        /// <summary>
        /// Gets the role assignments.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<RoleAssignmentDto>>> GetRoleAssignmentsAsync(string username)
        {
            var applicationUser = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (applicationUser == null || applicationUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError<List<RoleAssignmentDto>>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var rolesOfUser = await _userManager.GetRolesAsync(applicationUser).ConfigureAwait(false);
            var allRoles = _roleManager.Roles.ToList();

            var response = new List<RoleAssignmentDto>();

            foreach (var role in allRoles)
            {
                var roleAssignment = new RoleAssignmentDto() { RoleName = role.Name };
                if (rolesOfUser.Contains(role.Name))
                {
                    roleAssignment.IsAssigned = true;
                }

                response.Add(roleAssignment);
            }

            return ServiceResponseHelper.SetSuccess(response);
        }

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<UserDto>> GetUserByUsernameAsync(string username)
        {
            var applicationUser = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (applicationUser == null || applicationUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var mappingResult = Mapper.Map<ApplicationUser, UserDto>(applicationUser);

            mappingResult.IsLocked = _userManager.IsLockedOutAsync(applicationUser).Result;

            return ServiceResponseHelper.SetSuccess(mappingResult);
        }

        /// <summary>
        /// Gets the user claims tree.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<ClaimTreeViewItemDto>>> GetUserClaimsTreeAsync(string userName)
        {
            var resultList = new List<ClaimTreeViewItemDto>();
            if (userName == null)
            {
                return ServiceResponseHelper.SetSuccess(resultList);
            }

            userName = userName.ToLower(new CultureInfo("en"));

            var selectedUser = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            if (selectedUser == null)
            {
                return ServiceResponseHelper.SetSuccess(resultList);
            }

            var userClaimList = await _userManager.GetClaimsAsync(selectedUser).ConfigureAwait(false);
            resultList = ClaimTreeHelper.GetClaimsTree(resultList, userClaimList, _localize);

            return ServiceResponseHelper.SetSuccess(resultList);
        }

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="pagedRequest">The paged request.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<PagedResultDto<UserDto>>> GetUserListAsync(PagedRequestDto pagedRequest)
        {
            var query = _userManager.Users.Where(x => !x.IsDeleted);

            if (pagedRequest.Orders != null && pagedRequest.Orders.Any())
            {
                query = pagedRequest.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.UserName);
            }

            var users = await query
                                                    .ToPagedListAsync(pagedRequest.PageIndex, pagedRequest.PageSize)
                                                    .ConfigureAwait(false);

            var mappingResult = Mapper.Map<IPagedList<ApplicationUser>, PagedResultDto<UserDto>>(users);

            return ServiceResponseHelper.SetSuccess(mappingResult);
        }

        /// <summary>
        /// Determines whether [is captcha passed asynchronous] [the specified captcha].
        /// </summary>
        /// <param name="captcha">The captcha.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Documentation", "CA1200:Avoid using cref tags with a prefix", Justification = "error")]
        public async Task<ServiceResponse<bool>> IsCaptchaPassedAsync(string captcha)
        {
            var result = await _captchaValidator.IsCaptchaPassedAsync(captcha).ConfigureAwait(false);
            return ServiceResponseHelper.SetSuccess(result);
        }

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="loginRequest">The loginRequestDto.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<LoginDto>> LoginAsync(LoginRequestDto loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Email).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                await CreateLoginAuditLogAsync(Localize["InvalidLoginCredentials"], false, loginRequest.Email).ConfigureAwait(false);

                return ServiceResponseHelper.SetError<LoginDto>(null, Localize["InvalidLoginCredentials"], StatusCodes.Status400BadRequest, true);
            }

            var loginResponse = new LoginDto();
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            if (!user.EmailConfirmed && !isDevelopment)
            {
                await CreateLoginAuditLogAsync(Localize["ActivateEmailToLogin"], false, loginRequest.Email).ConfigureAwait(false);

                loginResponse.EmailConfirmed = false;
                return ServiceResponseHelper.SetError(loginResponse, Localize["ActivateEmailToLogin"], StatusCodes.Status400BadRequest, true);
            }

            if (!user.IsActive)
            {
                await CreateLoginAuditLogAsync(Localize["PassiveUser"], false, loginRequest.Email).ConfigureAwait(false);

                loginResponse.IsActive = false;
                return ServiceResponseHelper.SetError(loginResponse, Localize["PassiveUser"], StatusCodes.Status400BadRequest, true);
            }

            var signResult = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, lockoutOnFailure: true).ConfigureAwait(false);

            if (signResult.IsLockedOut)
            {
                await CreateLoginAuditLogAsync(Localize["IsLockedOut"], false, loginRequest.Email).ConfigureAwait(false);

                return ServiceResponseHelper.SetError<LoginDto>(null, Localize["IsLockedOut"], StatusCodes.Status400BadRequest, true);
            }

            if (!signResult.Succeeded)
            {
                await CreateLoginAuditLogAsync(Localize["InvalidLoginCredentials"], false, loginRequest.Email).ConfigureAwait(false);

                return ServiceResponseHelper.SetError<LoginDto>(null, Localize["InvalidLoginCredentials"], StatusCodes.Status400BadRequest, true);
            }

            _ = int.TryParse(_configuration["Identity:Policy:Password:ExpireDays"], out var passwordExpireDays);

            if (user.LastPasswordChangedDate.AddDays(passwordExpireDays) < DateTime.UtcNow)
            {
                await CreateLoginAuditLogAsync(Localize["PasswordExpired"], false, loginRequest.Email).ConfigureAwait(false);

                return ServiceResponseHelper.SetError(new LoginDto { PasswordExpired = true, }, Localize["PasswordExpired"], StatusCodes.Status400BadRequest, true);
            }

            var claimsInfo = await GetUserClaimsAsync(user).ConfigureAwait(false);

            int? expireInMinutes = null;
            int? refreshTokenExpireInMinutes = null;

            if (loginRequest.RememberMe)
            {
                if (!int.TryParse(_configuration["Identity:Jwt:RefreshTokenExpiration"], out var configRefreshTokenExpireInMinutes))
                {
                    return ServiceResponseHelper.SetError<LoginDto>(null, "InvalidRefreshTokenExpiration", StatusCodes.Status500InternalServerError, true);
                }

                if (!int.TryParse(_configuration["Identity:Jwt:RememberMeSlidingExpireInMinute"], out var rememberMeSlidingExpireInMinute))
                {
                    return ServiceResponseHelper.SetError<LoginDto>(null, "RememberMeSlidingExpireInMinute", StatusCodes.Status500InternalServerError, true);
                }

                expireInMinutes = rememberMeSlidingExpireInMinute;
                refreshTokenExpireInMinutes = rememberMeSlidingExpireInMinute + configRefreshTokenExpireInMinutes;
            }

            var token = loginRequest.RememberMe ? _tokenHelper.BuildToken(claimsInfo.Result, expireInMinutes, refreshTokenExpireInMinutes) : _tokenHelper.BuildToken(claimsInfo.Result);
            if (token.IsSuccessful)
            {
                await CreateLoginAuditLogAsync(Localize["LoginSuccess"], true, loginRequest.Email).ConfigureAwait(false);

                var mappingClaimsInfo = claimsInfo.Result.Select(i => new ClaimDto() { Name = i.Type, Value = i.Value }).ToList();
                loginResponse = new LoginDto() { Token = token.Result.Token, Claims = mappingClaimsInfo, RefreshToken = token.Result.RefreshToken };
                return ServiceResponseHelper.SetSuccess(loginResponse);
            }

            await CreateLoginAuditLogAsync(Localize["LoginSuccess"], true, loginRequest.Email).ConfigureAwait(false);

            return ServiceResponseHelper.SetError(loginResponse, token.Error, true);
        }

        public ServiceResponse<AuthenticationProperties> GetAuthenticationProperties(string provider, string redirectUrl)
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return ServiceResponseHelper.SetSuccess(properties);
        }

        public async Task<ServiceResponse<LoginDto>> ExternalLoginAsync()
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync().ConfigureAwait(false);

            var processUser = new ApplicationUser
            {
                Email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                Name = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.GivenName),
                Surname = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Surname)
            };

            var userEntity = await _userManager.FindByNameAsync(processUser.Email).ConfigureAwait(false);
            if (userEntity == null)
            {
                string password = PasswordGenerator.Generate(
                     _passwordOptions.RequiredLength,
                     _passwordOptions.RequiredUniqueChars,
                     _passwordOptions.RequireDigit,
                     _passwordOptions.RequireLowercase,
                     _passwordOptions.RequireNonAlphanumeric,
                     _passwordOptions.RequireUppercase);

                var userDto = new UserDto
                {
                    Email = processUser.Email,
                    Password = password,
                    Name = processUser.Name,
                    Surname = processUser.Surname,
                    IsActive = true,
                    EmailConfirmed = true
                };

                var createdUserResponse = await CreateUserAsync(userDto).ConfigureAwait(false);

                if (createdUserResponse.IsSuccessful)
                {
                    processUser = createdUserResponse.Result;
                }
            }
            else
            {
                processUser = userEntity;
            }

            await _userManager.SetAuthenticationTokenAsync(processUser, externalLoginInfo.LoginProvider, processUser.UserName, externalLoginInfo.Principal.FindFirstValue(ClaimTypes.NameIdentifier)).ConfigureAwait(false);

            var claimsInfo = await GetUserClaimsAsync(processUser).ConfigureAwait(false);
            var token = _tokenHelper.BuildToken(claimsInfo.Result);
            if (token.IsSuccessful)
            {
                await CreateLoginAuditLogAsync(Localize["LoginSuccess"], true, processUser.Email).ConfigureAwait(false);

                var loginResponse = new LoginDto() { Token = token.Result.Token, RefreshToken = token.Result.RefreshToken };
                return ServiceResponseHelper.SetSuccess(loginResponse);
            }

            return ServiceResponseHelper.SetError(new LoginDto(), token.Error, true);
        }

        /// <summary>
        /// Posts the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <param name="requestScheme">The request scheme. (http https etc.)</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<UserDto>> PostAsync(UserDto userDto, string requestScheme, string insertedUser)
        {
            userDto.Password = PasswordGenerator.Generate(
                _passwordOptions.RequiredLength,
                _passwordOptions.RequiredUniqueChars,
                _passwordOptions.RequireDigit,
                _passwordOptions.RequireLowercase,
                _passwordOptions.RequireNonAlphanumeric,
                _passwordOptions.RequireUppercase);
            userDto.IsActive = true;
            userDto.InsertedUser = insertedUser;
            userDto.EmailConfirmed = true;

            var createdUser = await CreateUserAsync(userDto).ConfigureAwait(false);

            if (!createdUser.IsSuccessful)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, createdUser.Error, true);
            }

            await SendResetPasswordMailAsync(userDto.Email, requestScheme).ConfigureAwait(false);

            var mappingResult = Mapper.Map<UserDto>(createdUser.Result);

            return ServiceResponseHelper.SetSuccess(mappingResult);
        }

        /// <summary>
        /// Puts the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<UserDto>> PutAsync(string username, UserUpdateDto user, string updatedUser)
        {
            var dbUser = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (dbUser == null || dbUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (dbUser.PhoneNumber != user.PhoneNumber)
            {
                dbUser.PhoneNumberConfirmed = false;
            }

            dbUser.Email = user.Email;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.Name = user.Name;
            dbUser.Surname = user.Surname;
            dbUser.UpdatedDate = DateTime.UtcNow;
            dbUser.UpdatedUser = updatedUser;
            dbUser.TimeZone = user.TimeZone;

            var updatedResult = await _userManager.UpdateAsync(dbUser).ConfigureAwait(false);
            if (!updatedResult.Succeeded)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, $"{Localize["UnableToUpdateUser"]}{updatedResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
            }
            else
            {
                if (await _userManager.IsLockedOutAsync(dbUser).ConfigureAwait(false) && !user.IsLocked)
                {
                    await UnlockUserAsync(dbUser).ConfigureAwait(false);
                }
                else if (user.IsLocked)
                {
                    await LockUserAsync(dbUser).ConfigureAwait(false);
                }
            }

            var userResponse = Mapper.Map<ApplicationUser, UserDto>(dbUser);

            return ServiceResponseHelper.SetSuccess(userResponse);
        }

        /// <summary>
        /// Puts the roles specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="userRole">The user roles.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> PutForUserRoleAsync(string username, UserRoleUpdateDto userRole)
        {
            var dbUser = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (dbUser == null || dbUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var userDbRoles = await _userManager.GetRolesAsync(dbUser).ConfigureAwait(false);

            var rolesWillBeAdded = FindAssignedRolesToUser(userDbRoles, userRole);
            if (rolesWillBeAdded.Any())
            {
                var addToRolesResult = await _userManager.AddToRolesAsync(dbUser, rolesWillBeAdded).ConfigureAwait(false);

                if (!addToRolesResult.Succeeded)
                {
                    return ServiceResponseHelper.SetError($"{Localize["UnableToAddRolesToUser"]}{addToRolesResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
                }
            }

            var rolesWillBeDeleted = FindDeletedRolesFromUser(userDbRoles, userRole);
            if (rolesWillBeDeleted.Any())
            {
                var removeFromRolesResult = await _userManager.RemoveFromRolesAsync(dbUser, rolesWillBeDeleted).ConfigureAwait(false);

                if (!removeFromRolesResult.Succeeded)
                {
                    return ServiceResponseHelper.SetError($"{Localize["UnableToRemoveRolesFromUser"]}{removeFromRolesResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
                }
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Puts the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<UserDto>> PutForUserProfileAsync(string username, UserUpdateDto user)
        {
            var dbUser = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (dbUser == null || dbUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (dbUser.PhoneNumber != user.PhoneNumber)
            {
                dbUser.PhoneNumberConfirmed = false;
            }

            dbUser.Email = user.Email;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.Name = user.Name;
            dbUser.Surname = user.Surname;
            dbUser.TimeZone = user.TimeZone;

            var updatedResult = await _userManager.UpdateAsync(dbUser).ConfigureAwait(false);
            if (!updatedResult.Succeeded)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, $"{Localize["UnableToUpdateUser"]}{updatedResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
            }

            var userResponse = Mapper.Map<ApplicationUser, UserDto>(dbUser);

            return ServiceResponseHelper.SetSuccess(userResponse);
        }

        /// <summary>
        /// Refreshes the specified refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>Login response.</returns>
        public ServiceResponse<LoginDto> Refresh(string refreshToken)
        {
            var token = _tokenHelper.ValidateRefreshToken(refreshToken);

            var loginResponse = new LoginDto();
            if (token.IsSuccessful)
            {
                loginResponse.Token = token.Result.Token;
                loginResponse.RefreshToken = token.Result.RefreshToken;
                return ServiceResponseHelper.SetSuccess(loginResponse);
            }

            return ServiceResponseHelper.SetError(loginResponse, token.Error, true);
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="user">The model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse<RegisterDto>> RegisterAsync(UserDto user)
        {
            user.IsActive = true;
            var createdUser = await CreateUserAsync(user).ConfigureAwait(false);
            if (!createdUser.IsSuccessful)
            {
                return ServiceResponseHelper.SetError<RegisterDto>(null, createdUser.Error, true);
            }

            await _signInManager.SignInAsync(createdUser.Result, true).ConfigureAwait(false);

            var claimsInfo = await GetUserClaimsAsync(createdUser.Result).ConfigureAwait(false);

            var token = _tokenHelper.BuildToken(claimsInfo.Result);

            var loginResponse = new RegisterDto();
            if (token.IsSuccessful)
            {
                loginResponse.Token = token.Result.Token;
                loginResponse.RefreshToken = token.Result.RefreshToken;
                loginResponse.UserId = createdUser.Result.Id.ToString();
                loginResponse.Username = createdUser.Result.Email;

                return ServiceResponseHelper.SetSuccess(loginResponse);
            }

            return ServiceResponseHelper.SetError(loginResponse, token.Error, true);
        }

        /// <summary>
        /// Removes the claim from user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="claimValue">The claim value.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<bool>> RemoveClaimFromUserAsync(string username, string claimValue)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(false, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var claims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);

            if (claims == null)
            {
                return ServiceResponseHelper.SetError(false, Localize["ClaimNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (claims.All(c => c.Value != claimValue))
            {
                return ServiceResponseHelper.SetError(false, Localize["ClaimNotFound"], StatusCodes.Status204NoContent, true);
            }

            var result = await _userManager.RemoveClaimAsync(user, claims.First(c => c.Value == claimValue)).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(false, result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess(true);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse<string>> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email).ConfigureAwait(false);
            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError<string>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError<string>(null, $"{Localize["UnableToResetPassword"]}{result.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
            }

            user.EmailConfirmed = true;
            user.LastPasswordChangedDate = DateTime.UtcNow;
            _ = await _userManager.UpdateAsync(user).ConfigureAwait(false);
            await UnlockUserAsync(user).ConfigureAwait(false);

            return ServiceResponseHelper.SetSuccess(Localize["PasswordResetSuccessful"].ToString());
        }

        /// <summary>
        /// Saves the user claims.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> SaveUserClaimsAsync(SaveUserClaimsDto model)
        {
            if (model.SelectedUserClaimList == null)
            {
                model.SelectedUserClaimList = new List<string>(); // delete all claims for selected user
            }

            var selectedUser = await _userManager.FindByNameAsync(model.Name.ToLower(CultureInfo.CurrentCulture)).ConfigureAwait(false);
            if (selectedUser == null || selectedUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var userClaimListQuery = await _userManager.GetClaimsAsync(selectedUser).ConfigureAwait(false);

            var userClaimList = userClaimListQuery.Where(w => w.Type == ApplicationPolicyType.KsPermission).Select(s => s.Value).ToList();

            var deletedList = userClaimList.Where(item => !model.SelectedUserClaimList.Contains(item)).ToList();

            var permissionList = ClaimTreeHelper.SaveClaimsPreparePermissionList(deletedList, userClaimList);

            var addedList = model.SelectedUserClaimList.Where(item => !userClaimList.Contains(item)).ToList();

            addedList = addedList.Where(item => permissionList.Contains(item)).ToList();

            deletedList.AddRange(userClaimList.Where(item => !permissionList.Contains(item)).ToList());

            foreach (var claimName in deletedList)
            {
                _ = await _userManager.RemoveClaimAsync(selectedUser, new Claim(ApplicationPolicyType.KsPermission, claimName)).ConfigureAwait(false);
            }

            foreach (var claimName in addedList)
            {
                _ = await _userManager.AddClaimAsync(selectedUser, new Claim(ApplicationPolicyType.KsPermission, claimName)).ConfigureAwait(false);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Searches the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<PagedResultDto<UserDto>>> SearchAsync(UserSearchDto model)
        {
            var normalizeName = _keyNormalizer.NormalizeName(model.Username);

            var users = await _userManager.Users.Where(u => u.NormalizedUserName.StartsWith(normalizeName))
                                                    .ToPagedListAsync(model.PageIndex, model.PageSize).ConfigureAwait(false);

            var userGetResponse = Mapper.Map<IPagedList<ApplicationUser>, PagedResultDto<UserDto>>(users);
            return ServiceResponseHelper.SetSuccess(userGetResponse);
        }

        /// <summary>
        /// Sends reset password mail.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="scheme">The scheme.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> SendResetPasswordMailAsync(string email, string scheme)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var emailTemplate = await _emailTemplateService.GetEmailTemplateByNameAsync(EmailTemplateNameConstant.ForgotPassword).ConfigureAwait(false);

            if (emailTemplate == null)
            {
                return ServiceResponseHelper.SetError(Localize["EmailTemplateNotFound"], StatusCodes.Status204NoContent, true);
            }

            var currentTwoLetterCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            Enum.TryParse(typeof(Common.Enums.LanguageType), currentTwoLetterCulture, false, out object languageEnum);
            Common.Enums.LanguageType cultureLanguageEnum = (Common.Enums.LanguageType)languageEnum;

            var emailTemplateTranslation = emailTemplate.Result.Translations.FirstOrDefault(p => p.Language == cultureLanguageEnum);

            if (emailTemplateTranslation == null)
            {
                return ServiceResponseHelper.SetError(Localize["EmailTemplateTranslationNotFound"], StatusCodes.Status204NoContent, true);
            }

            var resetUrl = await GeneratePasswordResetUrlAsync(user, scheme).ConfigureAwait(false);
            string emailContent = emailTemplateTranslation.EmailContent.Replace("{resetUrl}", resetUrl, StringComparison.CurrentCulture);
            await _emailNotificationService.SendEmailAsync(emailTemplateTranslation.Subject, emailContent, email).ConfigureAwait(false);

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Creates email activation url by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> CreateEmailActivationUrlAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError<string>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
            token = System.Web.HttpUtility.UrlEncode(token);
            var activationUrl = $"{_configuration["EmailConfirmationSettings:Schema"]}://{_configuration["EmailConfirmationSettings:Host"]}/{_configuration["EmailConfirmationSettings:Controller"]}/{_configuration["EmailConfirmationSettings:Action"]}?token={token}&email={user.Email}";

            return ServiceResponseHelper.SetSuccess(activationUrl);
        }

        /// <summary>
        /// Creates email activation content.
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<EmailTemplateTranslationDto>> GetEmailActivationTemplateAsync(string url)
        {
            var emailTemplateResponse = await _emailTemplateService.GetEmailTemplateByNameAsync(EmailTemplateName.EmailActivation).ConfigureAwait(false);

            if (emailTemplateResponse.IsSuccessful)
            {
                var currentTwoLetterCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                Enum.TryParse(typeof(Common.Enums.LanguageType), currentTwoLetterCulture, false, out object languageEnum);
                Common.Enums.LanguageType cultureLanguageEnum = (Common.Enums.LanguageType)languageEnum;
                var emailTranslation = emailTemplateResponse.Result.Translations.FirstOrDefault(x => x.Language == cultureLanguageEnum);

                if (emailTranslation != null)
                {
                    emailTranslation.EmailContent = Regex.Replace(emailTranslation.EmailContent, @"RESET_URL", url);

                    return ServiceResponseHelper.SetSuccess(emailTranslation);
                }
                else
                {
                    return ServiceResponseHelper.SetError<EmailTemplateTranslationDto>(null, Localize["EmailTemplateNotFound"], StatusCodes.Status204NoContent, true);
                }
            }
            else
            {
                return ServiceResponseHelper.SetError<EmailTemplateTranslationDto>(null, Localize["EmailTemplateNotFound"], StatusCodes.Status204NoContent, true);
            }
        }

        /// <summary>
        /// Confirms email.
        /// </summary>
        /// <param name="email">The email that will be confimed</param>
        /// <param name="token">The email token.</param>
        /// <returns></returns>
        public async Task<ServiceResponse> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user == null || user.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (!user.EmailConfirmed)
            {
                var identityResult = await _userManager.ConfirmEmailAsync(user, token).ConfigureAwait(false);

                if (identityResult.Succeeded)
                {
                    return ServiceResponseHelper.SetSuccess();
                }
                else
                {
                    return ServiceResponseHelper.SetError(Localize["EmailCouldNotBeConfirmed"], StatusCodes.Status400BadRequest, true);
                }
            }
            else
            {
                return ServiceResponseHelper.SetError(Localize["EmailAlreadyConfirmed"], StatusCodes.Status400BadRequest, true);
            }
        }

        /// <summary>
        /// Update user profile photo.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="profilePhoto">The profile photo.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse<UserDto>> SetProfilePhotoAsync(string username, string profilePhoto)
        {
            var dbUser = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (dbUser == null || dbUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            dbUser.ProfilePhoto = profilePhoto;
            dbUser.UpdatedDate = DateTime.UtcNow;
            dbUser.UpdatedUser = username;

            var updatedResult = await _userManager.UpdateAsync(dbUser).ConfigureAwait(false);
            if (!updatedResult.Succeeded)
            {
                return ServiceResponseHelper.SetError<UserDto>(null, $"{Localize["UnableToUpdateUser"]}{updatedResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
            }

            var userResponse = Mapper.Map<ApplicationUser, UserDto>(dbUser);

            return ServiceResponseHelper.SetSuccess(userResponse);
        }

        /// <summary>
        /// Delete user profile photo.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse> DeleteProfilePhotoAsync(string username)
        {
            var dbUser = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (dbUser == null || dbUser.IsDeleted)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            dbUser.ProfilePhoto = null;

            var updatedResponse = await _userManager.UpdateAsync(dbUser).ConfigureAwait(false);
            if (!updatedResponse.Succeeded)
            {
                return ServiceResponseHelper.SetError(false, Localize["UserProfilePhotoNotUpdated"], StatusCodes.Status204NoContent, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Finds the assigned roles to user.
        /// </summary>
        /// <param name="userDbRoles">The user database roles.</param>
        /// <param name="userRole">The user request roles</param>
        /// <returns></returns>
        private static List<string> FindAssignedRolesToUser(IList<string> userDbRoles, UserRoleUpdateDto userRole)
        {
            var rolesWillBeAdded = new List<string>();

            if (userRole.AssignedRoles != null && userRole.AssignedRoles.Count > 0)
            {
                rolesWillBeAdded = userDbRoles.Any() ? userRole.AssignedRoles.Where(p => !userDbRoles.Contains(p)).ToList() : userRole.AssignedRoles;
            }

            return rolesWillBeAdded;
        }

        /// <summary>
        /// Finds the deleted roles from user.
        /// </summary>
        /// <param name="userDbRoles">The user database roles.</param>
        /// <param name="userRole">The user request roles.</param>
        /// <returns></returns>
        private static List<string> FindDeletedRolesFromUser(IList<string> userDbRoles, UserRoleUpdateDto userRole)
        {
            var rolesWillBeDeleted = new List<string>();

            if (userRole.UnassignedRoles != null && userRole.UnassignedRoles.Count > 0)
            {
                rolesWillBeDeleted = userDbRoles.Where(p => userRole.UnassignedRoles.Any(l => p == l)).ToList();
            }

            return rolesWillBeDeleted;
        }

        /// <summary>
        /// Gets the MAC address of the current PC.
        /// </summary>
        /// <returns>MacAddress information. </returns>
        private static string GetMacAddress()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }

            return string.Empty;
        }

        private async Task CreateLoginAuditLogAsync(string message, bool success, string requestUserName)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var dto = new LoginAuditLogDto
            {
                Success = success,
                Message = message,
                Ip = httpContext?.Connection?.RemoteIpAddress?.ToString(),
                Hostname = Dns.GetHostName(),
                MacAddress = GetMacAddress(),
                BrowserGuid = httpContext?.Request?.Cookies["AuthToken"] ?? httpContext?.Request?.Cookies["ASP.NET_SessionId"],
                BrowserDetail = httpContext?.Request?.Headers["User-Agent"].ToString(),
                RequestHeaderInfo = JsonConvert.SerializeObject(httpContext?.Request?.Headers),
                ApplicationUserName = Environment.UserName,
                OsName = System.Runtime.InteropServices.RuntimeInformation.OSDescription ?? "Unknown",
                RequestUserName = requestUserName
            };

            _ = await _loginAuditLogService.CreateAsync(dto).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userPostRequest">The user post request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task<ServiceResponse<ApplicationUser>> CreateUserAsync(UserDto userPostRequest)
        {
            var user = new ApplicationUser()
            {
                UserName = userPostRequest.Email,
                Email = userPostRequest.Email,
                PhoneNumber = userPostRequest.PhoneNumber,
                Name = userPostRequest.Name,
                Surname = userPostRequest.Surname,
                LastPasswordChangedDate = DateTime.UtcNow,
                EmailConfirmed = userPostRequest.EmailConfirmed,
                IsActive = userPostRequest.IsActive,
                IsDeleted = false,
                InsertedUser = userPostRequest.InsertedUser,
                InsertedDate = DateTime.UtcNow,
                TimeZone = userPostRequest.TimeZone
            };
            var result = await _userManager.CreateAsync(user, userPostRequest.Password).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError<ApplicationUser>(null, $"{Localize["UnableToCreateUser"]}{result.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess(user);
        }

        /// <summary>
        /// Gets the user claims.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task<ServiceResponse<List<Claim>>> GetUserClaimsAsync(ApplicationUser user)
        {
            var infoClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Surname ?? string.Empty),
                new Claim(JwtClaimTypes.ZoneInfo, user.TimeZone ?? ConfigurationConstant.DefaultTimeZone),
            };

            var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var claims = new List<Claim>();
            if (userRoles.Any())
            {
                foreach (var roleName in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));

                    var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);
                    var userRoleClaims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);
                    claims = ClaimTreeHelper.AddClaimsToListofClaims(claims, userRoleClaims);
                }
            }

            var userClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
            claims = ClaimTreeHelper.AddClaimsToListofClaims(claims, userClaims);

            _claimManager.SetClaims(infoClaims, claims, user.Id.ToString());

            return ServiceResponseHelper.SetSuccess(infoClaims);
        }

        private async Task LockUserAsync(ApplicationUser user)
        {
            _ = int.TryParse(
                _configuration["Identity:Policy:Lockout:DefaultLockoutTimeSpan"],
                out var defaultLockoutTimeSpan);
            var timeSpan = TimeSpan.FromMinutes(defaultLockoutTimeSpan);
            var dateTime = DateTimeOffset.UtcNow.Add(timeSpan);
            _ = await _userManager.SetLockoutEndDateAsync(user, dateTime).ConfigureAwait(false);
        }

        /// <summary>
        /// Unlocks the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task UnlockUserAsync(ApplicationUser user)
        {
            if (await _userManager.IsLockedOutAsync(user).ConfigureAwait(false))
            {
                _ = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Generates new password reset url by user and schema.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="scheme">The schema (http, https etc.)</param>
        /// <returns></returns>
        private async Task<string> GeneratePasswordResetUrlAsync(ApplicationUser user, string scheme)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

            var resetUrl = $"{scheme}://{_configuration["ResetPasswordSettings:Host"]}/{_configuration["ResetPasswordSettings:Controller"]}/{_configuration["ResetPasswordSettings:Action"]}?token={token}&email={user.Email}";
            return resetUrl;
        }
    }
}