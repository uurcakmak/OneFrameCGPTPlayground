// <copyright file="ActionExecutionExtensions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OneFrameCGPTPlayground.Infrastructure.Extensions
{
    /// <summary>
    /// ActionExecutionExtensions.
    /// </summary>
    public static class ActionExecutionExtensions
    {
        /// <summary>
        /// Validates the model.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static List<ValidationResult> ValidateModel(this ActionExecutingContext context)
        {
            _ = context.ThrowIfNull(nameof(context));

            return context.ModelState.Keys
                    .SelectMany(key => context.ModelState[key].Errors
                    .Select(x => new ValidationResult(x.ErrorMessage))).ToList();
        }
    }
}
