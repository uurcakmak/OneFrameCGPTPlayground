// <copyright file="RoleService.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Common.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.Role;
using OneFrameCGPTPlayground.Application.Abstractions.Role.Contracts;
using OneFrameCGPTPlayground.Application.Abstractions.User.Contracts;
using OneFrameCGPTPlayground.Application.Helpers;
using OneFrameCGPTPlayground.Common.Authentication;
using OneFrameCGPTPlayground.Domain;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.Data.Relational;
using KocSistem.OneFrame.DesignObjects.Services;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Application.Role
{
    /// <summary>
    /// RoleService.
    /// </summary>
    /// <seealso cref="IRoleService" />
    public class RoleService : ApplicationServiceBase<RoleService>, IRoleService
    {
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IKsStringLocalizer<RoleService> _localize;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="keyNormalizer">The key normalizer.</param>
        public RoleService(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ILookupNormalizer keyNormalizer, IKsI18N i18N)
        : base(serviceProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _keyNormalizer = keyNormalizer;
            _localize = i18N.GetLocalizer<RoleService>();
        }

        ~RoleService()
        {
            _roleManager.Dispose();
            _userManager.Dispose();
        }

        /// <summary>
        /// Adds the claim to role.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> AddClaimToRoleAsync(RoleClaimDto dto)
        {
            var role = await _roleManager.FindByNameAsync(dto.RoleName).ConfigureAwait(false);

            if (role == null)
            {
                return ServiceResponseHelper.SetError(Localize["RoleNotFound", dto.RoleName], StatusCodes.Status204NoContent, true);
            }

            var roleClaims = await GetClaimsInRoleAsync(dto.RoleName).ConfigureAwait(false);

            if (roleClaims.Result.Exists(x => x.Name == dto.Name))
            {
                return ServiceResponseHelper.SetError(Localize["ClaimAlreadyExist", dto.Name], StatusCodes.Status204NoContent, true);
            }

            var result = await _roleManager.AddClaimAsync(role, new Claim("roleClaim", dto.Name)).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Adds the user to role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A Task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> AddUserToRoleAsync(string roleName, string username)
        {
            var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (role == null)
            {
                return ServiceResponseHelper.SetError(Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if (user == null)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound", username], StatusCodes.Status204NoContent, true);
            }

            var result = await _userManager.AddToRoleAsync(user, roleName).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Deletes the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> DeleteAsync(string roleName)
        {
            var roleToDelete = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (roleToDelete == null)
            {
                return ServiceResponseHelper.SetError(Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var result = await _roleManager.DeleteAsync(roleToDelete).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Gets the application roles.
        /// </summary>
        /// <returns>List ApplicationRoleDto.</returns>
        public ServiceResponse<List<ApplicationRoleDto>> GetApplicationRoles()
        {
            var roles = _roleManager.Roles.Include(i => i.Translations).ToList();

            var mappingResult = Mapper.Map<List<ApplicationRoleDto>>(roles);
            return ServiceResponseHelper.SetSuccess(mappingResult);
        }

        /// <summary>
        /// Gets the application roles.
        /// </summary>
        /// <param name="pagedRequestDto">The paged request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse<PagedResultDto<ApplicationRoleDto>>> GetApplicationRolesAsync(PagedRequestDto pagedRequestDto)
        {
            var query = _roleManager.Roles.Include(i => i.Translations).AsQueryable();

            if (pagedRequestDto.Orders != null && pagedRequestDto.Orders.Any())
            {
                query = pagedRequestDto.Orders.Aggregate(query, (current, order) => order.DirectionDesc ? current.OrderByDescending(order.ColumnName) : current.OrderBy(order.ColumnName));
            }
            else
            {
                query = query.OrderBy(o => o.Name);
            }

            var roles = await query
                                                    .ToPagedListAsync(pagedRequestDto.PageIndex, pagedRequestDto.PageSize)
                                                    .ConfigureAwait(false);

            if (roles == null || roles.TotalCount == 0)
            {
                return ServiceResponseHelper.SetError<PagedResultDto<ApplicationRoleDto>>(null, Localize["RoleNotFound"], StatusCodes.Status204NoContent, true);
            }

            var roleGetResponse = Mapper.Map<IPagedList<ApplicationRole>, PagedResultDto<ApplicationRoleDto>>(roles);
            return ServiceResponseHelper.SetSuccess(roleGetResponse);
        }

        /// <summary>
        /// Gets the claims in role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<ClaimDto>>> GetClaimsInRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (role == null)
            {
                return ServiceResponseHelper.SetError<List<ClaimDto>>(null, Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var claims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);

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
        /// Gets the name of the role by.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        /// A <see>
        ///     <cref>T:System.Threading.Tasks.Task</cref>
        /// </see>
        /// representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<ApplicationRoleDto>> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.Roles.Include(i => i.Translations).FirstOrDefaultAsync(f => f.NormalizedName == _keyNormalizer.NormalizeName(roleName)).ConfigureAwait(false);

            if (role == null)
            {
                return ServiceResponseHelper.SetError<ApplicationRoleDto>(null, Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var mappingResult = Mapper.Map<ApplicationRoleDto>(role);

            return ServiceResponseHelper.SetSuccess(mappingResult);
        }

        /// <summary>
        /// Gets the role claims tree.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<ClaimTreeViewItemDto>>> GetRoleClaimsTreeAsync(string roleName)
        {
            var resultList = new List<ClaimTreeViewItemDto>();
            if (roleName == null)
            {
                return ServiceResponseHelper.SetSuccess(resultList);
            }

            roleName = _keyNormalizer.NormalizeName(roleName);

            var selectedRole = _roleManager.Roles.FirstOrDefault(f => f.NormalizedName == roleName);
            if (selectedRole == null)
            {
                return ServiceResponseHelper.SetSuccess(resultList);
            }

            var roleClaimList = await _roleManager.GetClaimsAsync(selectedRole).ConfigureAwait(false);
            resultList = ClaimTreeHelper.GetClaimsTree(resultList, roleClaimList, _localize);

            return ServiceResponseHelper.SetSuccess(resultList);
        }

        /// <summary>
        /// Gets the role user information.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<UserRoleInfoDto>>> GetRoleUserInfoAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (role == null)
            {
                return ServiceResponseHelper.SetError<List<UserRoleInfoDto>>(null, Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false);
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName).ConfigureAwait(false);

            var usersInRoleResponse = Mapper.Map<IList<ApplicationUser>, List<UserRoleInfoDto>>(users);
            foreach (var user in usersInRole)
            {
                usersInRoleResponse.First(f => f.Id == user.Id.ToString()).IsInRole = true;
            }

            return ServiceResponseHelper.SetSuccess(usersInRoleResponse);
        }

        /// <summary>
        /// Gets the users in role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<List<UserDto>>> GetUsersInRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (role == null)
            {
                return ServiceResponseHelper.SetError<List<UserDto>>(null, Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var users = await _userManager.GetUsersInRoleAsync(roleName).ConfigureAwait(false);

            var mappingResult = Mapper.Map<List<UserDto>>(users);

            return ServiceResponseHelper.SetSuccess(mappingResult);
        }

        /// <summary>
        /// Posts the specified application role dto.
        /// </summary>
        /// <param name="applicationRoleDto">The application role dto.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse<ApplicationRoleDto>> PostAsync(ApplicationRoleDto applicationRoleDto)
        {
            var entity = Mapper.Map<ApplicationRole>(applicationRoleDto);
            var result = await _roleManager.CreateAsync(entity).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError<ApplicationRoleDto>(null, result.GetErrorInfoDescriptions(), StatusCodes.Status400BadRequest, true);
            }

            var dto = Mapper.Map<ApplicationRoleDto>(entity);

            return ServiceResponseHelper.SetSuccess(dto);
        }

        /// <summary>
        /// Puts the specified role.
        /// </summary>
        /// <param name="roleUpdateDto">The role.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse<ApplicationRoleDto>> PutAsync(RoleUpdateDto roleUpdateDto)
        {
            var roleToUpdate = await _roleManager.Roles.Include(i => i.Translations).FirstOrDefaultAsync(f => f.NormalizedName == _keyNormalizer.NormalizeName(roleUpdateDto.RoleName)).ConfigureAwait(false);

            if (roleToUpdate == null)
            {
                return ServiceResponseHelper.SetError<ApplicationRoleDto>(null, Localize["RoleNotFound", roleUpdateDto.RoleName], StatusCodes.Status204NoContent, true);
            }

            foreach (var roleTranslate in roleUpdateDto.Translations)
            {
                var translationItem = roleToUpdate.Translations.FirstOrDefault(f => f.Language == roleTranslate.Language);
                if (translationItem == null)
                {
                    var newTranslation = Mapper.Map<ApplicationRoleTranslation>(roleTranslate);
                    roleToUpdate.Translations.Add(newTranslation);
                }
                else
                {
                    translationItem.Description = roleTranslate.Description;
                    translationItem.DisplayText = roleTranslate.DisplayText;
                }
            }

            var updateResult = await _roleManager.UpdateAsync(roleToUpdate).ConfigureAwait(false);
            if (!updateResult.Succeeded)
            {
                return ServiceResponseHelper.SetError<ApplicationRoleDto>(null, $"{Localize["UnableToUpdateRole"]}{updateResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
            }

            if (roleUpdateDto.UsersInRole is { Count: > 0 })
            {
                var addUsersToRoleResult = await AddUsersToRoleAsync(roleUpdateDto, roleToUpdate).ConfigureAwait(false);
                if (!addUsersToRoleResult.IsSuccessful)
                {
                    return ServiceResponseHelper.SetError<ApplicationRoleDto>(null, addUsersToRoleResult.Error, true);
                }
            }

            if (roleUpdateDto.UsersNotInRole != null && roleUpdateDto.UsersNotInRole.Count > 0)
            {
                var removeUsersFromRoleResult = await RemoveUsersFromRoleAsync(roleUpdateDto, roleToUpdate).ConfigureAwait(false);
                if (!removeUsersFromRoleResult.IsSuccessful)
                {
                    return ServiceResponseHelper.SetError<ApplicationRoleDto>(null, removeUsersFromRoleResult.Error, true);
                }
            }

            var result = Mapper.Map<ApplicationRole, ApplicationRoleDto>(roleToUpdate);

            return ServiceResponseHelper.SetSuccess(result);
        }

        /// <summary>
        /// Removes the claim from role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="claimvalue">The claimvalue.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> RemoveClaimFromRoleAsync(string roleName, string claimvalue)
        {
            var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (role == null)
            {
                return ServiceResponseHelper.SetError(Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var claims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);

            if (claims == null)
            {
                return ServiceResponseHelper.SetError(Localize["ClaimNotFound"], StatusCodes.Status204NoContent, true);
            }

            if (claims.All(c => c.Value != claimvalue))
            {
                return ServiceResponseHelper.SetError(Localize["ClaimNotFound"], StatusCodes.Status204NoContent, true);
            }

            var result = await _roleManager.RemoveClaimAsync(role, claims.First(c => c.Value == claimvalue)).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Removes the user from role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A task representing the asynchronous operation.
        /// </returns>
        public async Task<ServiceResponse> RemoveUserFromRoleAsync(string roleName, string username)
        {
            var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);
            if (role == null)
            {
                return ServiceResponseHelper.SetError(Localize["RoleNotFound", roleName], StatusCodes.Status204NoContent, true);
            }

            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);
            if (user == null)
            {
                return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                return ServiceResponseHelper.SetError(result.GetErrorInfosMessages(), StatusCodes.Status400BadRequest, true);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Saves the role claims.
        /// </summary>
        /// <param name="saveRoleClaimsDto">The model.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse> SaveRoleClaimsAsync(SaveRoleClaimsDto saveRoleClaimsDto)
        {
            if (saveRoleClaimsDto.SelectedRoleClaimList == null)
            {
                saveRoleClaimsDto.SelectedRoleClaimList = new List<string>(); // delete all claims for selected role
            }

            var selectedRole = _roleManager.Roles.FirstOrDefault(f => f.NormalizedName == _keyNormalizer.NormalizeName(saveRoleClaimsDto.Name));

            if (selectedRole == null)
            {
                return ServiceResponseHelper.SetError(Localize["RoleNotFound", selectedRole], StatusCodes.Status204NoContent, true);
            }

            var roleClaimListQuery = await _roleManager.GetClaimsAsync(selectedRole).ConfigureAwait(false);
            var roleClaimList = roleClaimListQuery.Where(w => w.Type == ApplicationPolicyType.KsPermission).Select(s => s.Value).ToList();

            var deletedList = roleClaimList.Where(item => !saveRoleClaimsDto.SelectedRoleClaimList.Contains(item)).ToList();

            var permissionList = ClaimTreeHelper.SaveClaimsPreparePermissionList(deletedList, roleClaimList);

            var addedList = saveRoleClaimsDto.SelectedRoleClaimList.Where(item => !roleClaimList.Contains(item)).ToList();

            addedList = addedList.Where(item => permissionList.Contains(item)).ToList();

            deletedList.AddRange(roleClaimList.Where(item => !permissionList.Contains(item)).ToList());

            foreach (var claimName in deletedList)
            {
                _ = await _roleManager.RemoveClaimAsync(selectedRole, new Claim(ApplicationPolicyType.KsPermission, claimName)).ConfigureAwait(false);
            }

            foreach (var claimName in addedList)
            {
                _ = await _roleManager.AddClaimAsync(selectedRole, new Claim(ApplicationPolicyType.KsPermission, claimName)).ConfigureAwait(false);
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Searches the specified role get request.
        /// </summary>
        /// <param name="roleSearchDto">The role get request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<ServiceResponse<PagedResultDto<ApplicationRoleDto>>> SearchAsync(RoleSearchDto roleSearchDto)
        {
            var normalizeName = _keyNormalizer.NormalizeName(roleSearchDto.Name);

            var roles = await _roleManager.Roles.Include(i => i.Translations).Where(u => u.NormalizedName.StartsWith(normalizeName))
                .ToPagedListAsync(roleSearchDto.PageIndex, roleSearchDto.PageSize).ConfigureAwait(false);

            var roleGetResponse = Mapper.Map<IPagedList<ApplicationRole>, PagedResultDto<ApplicationRoleDto>>(roles);
            return ServiceResponseHelper.SetSuccess(roleGetResponse);
        }

        /// <summary>
        /// Adds the users to role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="roleToUpdate">The role to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task<ServiceResponse> AddUsersToRoleAsync(RoleUpdateDto role, ApplicationRole roleToUpdate)
        {
            foreach (var user in role.UsersInRole)
            {
                var foundUser = await _userManager.FindByIdAsync(user).ConfigureAwait(false);

                if (foundUser == null)
                {
                    return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
                }

                if (!await _userManager.IsInRoleAsync(foundUser, roleToUpdate.Name).ConfigureAwait(false))
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(foundUser, roleToUpdate.Name).ConfigureAwait(false);
                    if (!addToRoleResult.Succeeded)
                    {
                        return ServiceResponseHelper.SetError($"{Localize["UnableToAddUserToRole"]}{addToRoleResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
                    }
                }
            }

            return ServiceResponseHelper.SetSuccess();
        }

        /// <summary>
        /// Removes the users from role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="roleToUpdate">The role to update.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private async Task<ServiceResponse> RemoveUsersFromRoleAsync(RoleUpdateDto role, ApplicationRole roleToUpdate)
        {
            foreach (var user in role.UsersNotInRole)
            {
                var foundUser = await _userManager.FindByIdAsync(user).ConfigureAwait(false);

                if (foundUser == null)
                {
                    return ServiceResponseHelper.SetError(Localize["UserNotFound"], StatusCodes.Status204NoContent, true);
                }

                if (await _userManager.IsInRoleAsync(foundUser, roleToUpdate.Name).ConfigureAwait(false))
                {
                    var removeUserFromRoleResult = await _userManager.RemoveFromRoleAsync(foundUser, roleToUpdate.Name).ConfigureAwait(false);

                    if (!removeUserFromRoleResult.Succeeded)
                    {
                        return ServiceResponseHelper.SetError($"{Localize["UnableToRemoveUserFromRole"]}{removeUserFromRoleResult.GetErrorInfosMessages()}", StatusCodes.Status400BadRequest, true);
                    }
                }
            }

            return ServiceResponseHelper.SetSuccess();
        }
    }
}