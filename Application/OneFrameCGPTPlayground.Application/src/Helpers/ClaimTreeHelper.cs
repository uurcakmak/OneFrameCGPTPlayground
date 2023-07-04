// <copyright file="ClaimTreeHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Application.Abstractions.Account.Contracts;
using OneFrameCGPTPlayground.Common.Authentication;
using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.I18N;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.Application.Helpers
{
    /// <summary>
    /// ClaimTreeHelper.
    /// </summary>
    public static class ClaimTreeHelper
    {
        /// <summary>
        /// Adds the claims to listof claims.
        /// </summary>
        /// <param name="claims">The claims.</param>
        /// <param name="claimsFromManagers">The claims from managers.</param>
        /// <returns>List[Claim].</returns>
        public static List<Claim> AddClaimsToListofClaims(List<Claim> claims, IList<Claim> claimsFromManagers)
        {
            _ = claims.ThrowIfNull(nameof(claims));
            if (claimsFromManagers != null && claimsFromManagers.Any())
            {
                foreach (var claim in claimsFromManagers)
                {
                    var claimItem = new Claim(claim.Type, claim.Value);
                    if (!claims.Exists(e => e.Type == claimItem.Type && e.Value == claimItem.Value))
                    {
                        claims.Add(claimItem);
                    }
                }
            }

            return claims;
        }

        /// <summary>
        /// Gets the claims tree.
        /// </summary>
        /// <param name="resultList">The result list.</param>
        /// <param name="claimList">The claim list.</param>
        /// <returns>List[ClaimTreeViewItemDto].</returns>
        public static List<ClaimTreeViewItemDto> GetClaimsTree(List<ClaimTreeViewItemDto> resultList, IList<Claim> claimList, IKsStringLocalizer<object> localize)
        {
            _ = resultList.ThrowIfNull(nameof(resultList));
            var permissionList = CreatePermissionList();

            foreach (var permissionName in permissionList.Keys.OrderBy(o => o))
            {
                var permissionSplit = permissionName.Split('_');
                ClaimTreeViewItemDto rootItem = null;

                var parentName = permissionSplit[0];
                if (resultList.All(a => a.Text != parentName))
                {
                    rootItem = new ClaimTreeViewItemDto
                    {
                        Text = parentName,
                        Id = permissionName,
                        State = new ClaimTreeViewItemStateInfoDto
                        {
                            Opened = !permissionList[permissionName],
                            Disabled = permissionList[permissionName],
                            Selected = false,
                        },
                    };
                    resultList.Add(rootItem);
                }
                else
                {
                    rootItem = resultList.First(f => f.Text == parentName);
                }

                ClaimTreeViewItemDto childItem;
                if (permissionSplit.Length < 2)
                {
                    continue;
                }

                var childName = permissionSplit[1];
                var newChild = true;
                if (rootItem.Children.All(a => a.Text != childName))
                {
                    childItem = CreateChildItem(childName, permissionName, claimList, permissionList, rootItem);
                }
                else
                {
                    childItem = rootItem.Children.First(a => a.Text == childName);
                    newChild = false;
                }

                GetRoleClaimsTreeChildrenItems(ref childItem, permissionSplit.Skip(2), permissionName, permissionList[permissionName], ref claimList, localize);
                if (newChild)
                {
                    rootItem.Children.Add(childItem);
                }

                if (rootItem.Children.Any())
                {
                    rootItem.Id = null;
                    rootItem.State.Selected = false;
                }
            }

            return resultList;
        }

        /// <summary>
        /// Saves the claims prepare permission list.
        /// </summary>
        /// <param name="deletedList">The deleted list.</param>
        /// <param name="claimList">The claim list.</param>
        /// <returns>List[string].</returns>
        public static List<string> SaveClaimsPreparePermissionList(List<string> deletedList, List<string> claimList)
        {
            _ = deletedList.ThrowIfNull(nameof(deletedList));
            _ = claimList.ThrowIfNull(nameof(claimList));
            var permissionList = new List<string>();

            foreach (var propertyInfo in typeof(KsPermissionPolicy).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var permissionName = propertyInfo.GetValue(null)?.ToString();
                if (permissionName != null)
                {
                    if (propertyInfo.CustomAttributes.FirstOrDefault(f => f.AttributeType == typeof(ObsoleteAttribute)) != null)
                    {
                        if (deletedList.Find(f => f == permissionName) == null && claimList.Find(f => f == permissionName) != null)
                        {
                            deletedList.Add(permissionName);
                        }
                    }
                    else
                    {
                        permissionList.Add(permissionName);
                    }
                }
            }

            return permissionList;
        }

        /// <summary>
        /// Creates the child item.
        /// </summary>
        /// <param name="childName">Name of the child.</param>
        /// <param name="permissionName">Name of the permission.</param>
        /// <param name="claimList">The claim list.</param>
        /// <param name="permissionList">The permission list.</param>
        /// <param name="rootItem">The root item.</param>
        /// <returns>ClaimTreeViewItemDto.</returns>
        private static ClaimTreeViewItemDto CreateChildItem(string childName, string permissionName, IList<Claim> claimList, Dictionary<string, bool> permissionList, ClaimTreeViewItemDto rootItem)
        {
            return new ClaimTreeViewItemDto
            {
                Text = childName,
                Id = permissionName,
                State = new ClaimTreeViewItemStateInfoDto
                {
                    Selected = claimList.Any(f => f.Type == ApplicationPolicyType.KsPermission && f.Value == permissionName),
                    Opened = !permissionList[permissionName],
                    Disabled = rootItem.State.Disabled || permissionList[permissionName],
                },
            };
        }

        /// <summary>
        /// Creates the permission list.
        /// </summary>
        /// <returns>Dictionary[string, bool].</returns>
        private static Dictionary<string, bool> CreatePermissionList()
        {
            var permissionList = new Dictionary<string, bool>();
            foreach (var propertyInfo in typeof(KsPermissionPolicy).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var permissionName = propertyInfo.GetValue(null)?.ToString();
                if (permissionName != null)
                {
                    permissionList.Add(permissionName, propertyInfo.CustomAttributes.FirstOrDefault(f => f.AttributeType == typeof(ObsoleteAttribute)) != null);
                }
            }

            return permissionList;
        }

        /// <summary>
        /// Gets the role claims tree children items.
        /// </summary>
        /// <param name="rootItem">The root item.</param>
        /// <param name="permissionSplit">The permission split.</param>
        /// <param name="permissionName">Name of the permission.</param>
        /// <param name="permissionDisabled">if set to <c>true</c> [permission disabled].</param>
        /// <param name="claimList">The claim list.</param>
        private static void GetRoleClaimsTreeChildrenItems(ref ClaimTreeViewItemDto rootItem, IEnumerable<string> permissionSplit, string permissionName, bool permissionDisabled, ref IList<Claim> claimList, IKsStringLocalizer<object> localize)
        {
            ClaimTreeViewItemDto childItem;
            var strings = permissionSplit as string[] ?? permissionSplit.ToArray();

            var newChild = true;
            if (strings.Length == 0)
            {
                return;
            }

            var childName = strings[0];
            if (rootItem.Children.All(a => a.Text != childName))
            {
                childItem = new ClaimTreeViewItemDto
                {
                    Text = childName,
                    Id = permissionName,
                    State = new ClaimTreeViewItemStateInfoDto
                    {
                        Selected = claimList.Any(f => f.Type == ApplicationPolicyType.KsPermission && f.Value == permissionName),
                        Opened = !permissionDisabled,
                        Disabled = rootItem.State.Disabled || permissionDisabled,
                    },
                };
            }
            else
            {
                childItem = rootItem.Children.First(a => a.Text == childName);
                newChild = false;
            }

            GetRoleClaimsTreeChildrenItems(ref childItem, strings.Skip(1), permissionName, permissionDisabled, ref claimList, localize);
            if (newChild)
            {
                childItem.Text += $"({localize[permissionName]})";
                rootItem.Children.Add(childItem);
            }

            if (!rootItem.Children.Any())
            {
                return;
            }

            rootItem.Id = null;
            rootItem.State.Selected = false;
        }
    }
}
