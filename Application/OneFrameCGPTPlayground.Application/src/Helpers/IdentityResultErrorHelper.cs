// <copyright file="IdentityResultErrorHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.DesignObjects.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace OneFrameCGPTPlayground.Application.Helpers
{
    /// <summary>
    /// IdentityResultErrorHelper.
    /// </summary>
    public static class IdentityResultErrorHelper
    {
        /// <summary>
        /// Gets the error information descriptions.
        /// </summary>
        /// <param name="identityResult">The identity result.</param>
        /// <returns>string.</returns>
        public static string GetErrorInfoDescriptions(this IdentityResult identityResult)
        {
            _ = identityResult.ThrowIfNull(nameof(identityResult));

            var result = new StringBuilder();

            foreach (var error in identityResult.Errors)
            {
                _ = result.AppendLine($"{error.Description}");
            }

            return result.ToString();
        }

        /// <summary>
        /// Gets the error infos.
        /// </summary>
        /// <param name="identityResult">The identity result.</param>
        /// <returns>List{ErrorInfo}.</returns>
        public static List<ErrorInfo> GetErrorInfos(this IdentityResult identityResult)
        {
            var errorInfoList = new List<ErrorInfo>();

            foreach (var error in identityResult.Errors)
            {
                var errorInfo = new ErrorInfo(error.Code, error.Description);
                errorInfoList.Add(errorInfo);
            }

            return errorInfoList;
        }

        /// <summary>
        /// Gets the error infos messages.
        /// </summary>
        /// <param name="identityResult">The identity result.</param>
        /// <returns>string.</returns>
        public static string GetErrorInfosMessages(this IdentityResult identityResult)
        {
            var errorInfos = GetErrorInfos(identityResult);

            var result = new StringBuilder();

            foreach (var errorInfo in errorInfos)
            {
                _ = result.AppendLine($"- {errorInfo.Details}");
            }

            return result.ToString();
        }
    }
}
