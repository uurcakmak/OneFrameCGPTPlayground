// <copyright file="ValidateModelAttribute.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.ErrorHandling;
using Microsoft.AspNetCore.Mvc.Filters;
using OneFrameCGPTPlayground.Infrastructure.Extensions;

namespace OneFrameCGPTPlayground.WebAPI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private readonly IKsStringLocalizer<ValidateModelAttribute> _localize;

        public ValidateModelAttribute(IKsI18N i18N)
        {
            _localize = i18N.GetLocalizer<ValidateModelAttribute>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationResults = context.ValidateModel();

                throw new OneFrameValidationException(_localize["InvalidModel"], validationResults);
            }
        }
    }
}