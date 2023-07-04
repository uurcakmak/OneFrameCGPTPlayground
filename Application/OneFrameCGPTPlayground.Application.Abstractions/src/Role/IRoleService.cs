// <copyright file="IRoleService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using KocSistem.OneFrame.DesignObjects;
using KocSistem.OneFrame.DesignObjects.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Abstractions.Role
{
    /// <summary>
    /// IRoleService.
    /// </summary>
    /// <seealso cref="IApplicationService" />
    public interface IRoleService : IApplicationService
    {
        /// <summary>
        /// Adds the claim to role.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> AddClaimToRoleAsync(RoleClaimDto dto);

        /// <summary>
        /// Adds the user to role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> AddUserToRoleAsync(string roleName, string username);

        /// <summary>
        /// Deletes the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> DeleteAsync(string roleName);

        /// <summary>
        /// Gets the application roles.
        /// </summary>
        /// <returns></returns>
        ServiceResponse<List<ApplicationRoleDto>> GetApplicationRoles();

        /// <summary>
        /// Gets the application roles.
        /// </summary>
        /// <param name="pagedRequestDto">The paged request dto.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<PagedResultDto<ApplicationRoleDto>>> GetApplicationRolesAsync(PagedRequestDto pagedRequestDto);

        /// <summary>
        /// Gets the claims in role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<List<ClaimDto>>> GetClaimsInRoleAsync(string roleName);

        /// <summary>
        /// Gets the name of the role by.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<ApplicationRoleDto>> GetRoleByNameAsync(string roleName);

        /// <summary>
        /// Gets the role claims tree.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<List<ClaimTreeViewItemDto>>> GetRoleClaimsTreeAsync(string roleName);

        /// <summary>
        /// Gets the role user information.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<List<UserRoleInfoDto>>> GetRoleUserInfoAsync(string roleName);

        /// <summary>
        /// Gets the users in role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<List<UserDto>>> GetUsersInRoleAsync(string roleName);

        /// <summary>
        /// Posts the specified application role dto.
        /// </summary>
        /// <param name="applicationRoleDto">The application role dto.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<ApplicationRoleDto>> PostAsync(ApplicationRoleDto applicationRoleDto);

        /// <summary>
        /// Puts the specified role update dto.
        /// </summary>
        /// <param name="roleUpdateDto">The role update dto.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<ApplicationRoleDto>> PutAsync(RoleUpdateDto roleUpdateDto);

        /// <summary>
        /// Removes the claim from role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="claimvalue">The claimvalue.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> RemoveClaimFromRoleAsync(string roleName, string claimvalue);

        /// <summary>
        /// Removes the user from role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="username">The username.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> RemoveUserFromRoleAsync(string roleName, string username);

        /// <summary>
        /// Saves the role claims.
        /// </summary>
        /// <param name="saveRoleClaimsDto">The save role claims dto.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse> SaveRoleClaimsAsync(SaveRoleClaimsDto saveRoleClaimsDto);

        /// <summary>
        /// Searches the specified role search dto.
        /// </summary>
        /// <param name="roleSearchDto">The role search dto.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ServiceResponse<PagedResultDto<ApplicationRoleDto>>> SearchAsync(RoleSearchDto roleSearchDto);
    }
}