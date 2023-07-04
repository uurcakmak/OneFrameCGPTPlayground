// <copyright file="IAccountService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.EmailTemplateTranslation.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.Account
{
    /// <summary>
    /// IAccountService.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IAccountService : IApplicationService
    {
        /// <summary>
        /// Adds the claim to user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="claim">The claim.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> AddClaimToUserAsync(string username, string claim);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> ChangePasswordAsync(string username, string currentPassword, string newPassword);

        /// <summary>
        /// Changes the expired password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> ChangePasswordExpiredAsync(string username, string currentPassword, string newPassword);

        /// <summary>
        /// Confirms email.
        /// </summary>
        /// <param name="email">The email that will be confimed</param>
        /// <param name="token">The email token.</param>
        /// <returns></returns>
        Task<ServiceResponse> ConfirmEmailAsync(string email, string token);

        /// <summary>
        /// Creates email activation url by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<string>> CreateEmailActivationUrlAsync(string username);

        /// <summary>
        /// Delete profile photo of user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> DeleteProfilePhotoAsync(string username);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="updatedUserName">The updated username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> DeleteUserAsync(string username, string updatedUserName);

        /// <summary>
        /// External logins the specified provider.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<LoginDto>> ExternalLoginAsync();

        /// <summary>
        /// Get authentication properties
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="redirectUrl"></param>
        /// <returns>representing the synchronous operation.</returns>
        ServiceResponse<AuthenticationProperties> GetAuthenticationProperties(string provider, string redirectUrl);

        /// <summary>
        /// Gets the claims in user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<List<ClaimDto>>> GetClaimsInUserAsync(string username);

        /// <summary>
        /// Gets email activation template.
        /// </summary>
        /// <param name="url">The activation url that can be injected.</param>
        /// <returns></returns>
        Task<ServiceResponse<EmailTemplateTranslationDto>> GetEmailActivationTemplateAsync(string url);

        /// <summary>
        /// Gets the role assignments.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<List<RoleAssignmentDto>>> GetRoleAssignmentsAsync(string username);

        /// <summary>
        /// Gets the user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<UserDto>> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Gets the user claims tree.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<List<ClaimTreeViewItemDto>>> GetUserClaimsTreeAsync(string userName);

        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <param name="pagedRequest">The paged request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<PagedResultDto<UserDto>>> GetUserListAsync(PagedRequestDto pagedRequest);

        /// <summary>
        /// Determines whether [is captcha passed asynchronous] [the specified captcha].
        /// </summary>
        /// <param name="captcha">The captcha.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<bool>> IsCaptchaPassedAsync(string captcha);

        /// <summary>
        /// Logins the specified email.
        /// </summary>
        /// <param name="loginRequest">The paged request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<LoginDto>> LoginAsync(LoginRequestDto loginRequest);

        /// <summary>
        /// Posts the specified user dto.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        /// <param name="requestScheme">The request scheme.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<UserDto>> PostAsync(UserDto userDto, string requestScheme, string insertedUser);

        /// <summary>
        /// Puts the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="user">The user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<UserDto>> PutAsync(string username, UserUpdateDto user, string updatedUser);

        /// <summary>
        /// Puts the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="user">The user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<UserDto>> PutForUserProfileAsync(string username, UserUpdateDto user);

        /// <summary>
        /// Puts the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="userRole">The user roles.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> PutForUserRoleAsync(string username, UserRoleUpdateDto userRole);

        /// <summary>
        /// Refreshes the specified refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns></returns>
        ServiceResponse<LoginDto> Refresh(string refreshToken);

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<RegisterDto>> RegisterAsync(UserDto user);

        /// <summary>
        /// Removes the claim from user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="claimValue">The claim value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<bool>> RemoveClaimFromUserAsync(string username, string claimValue);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<string>> ResetPasswordAsync(ResetPasswordDto resetPassword);

        /// <summary>
        /// Saves the user claims.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> SaveUserClaimsAsync(SaveUserClaimsDto model);

        /// <summary>
        /// Searches the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<PagedResultDto<UserDto>>> SearchAsync(UserSearchDto model);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="scheme">The scheme.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> SendResetPasswordMailAsync(string email, string scheme);

        /// <summary>
        /// Update user profile photo.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="profilePhoto">The profile photo.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<UserDto>> SetProfilePhotoAsync(string username, string profilePhoto);
    }
}