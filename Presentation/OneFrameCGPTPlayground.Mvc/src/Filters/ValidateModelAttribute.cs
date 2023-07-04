// <copyright file="ValidateModelAttribute.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Infrastructure.Extensions;
using OneFrameCGPTPlayground.Mvc.Controllers;
using KocSistem.OneFrame.ErrorHandling;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace OneFrameCGPTPlayground.Mvc.Filters
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
                if (context.Controller is BaseController controller)
                {
                    var validationResults = context.ValidateModel();
                    context.Result = controller.ToastError(_localize["InvalidModel"], validationResults);
                }
                else
                {
                    throw new OneFrameEndUserException((int)HttpStatusCode.BadRequest, _localize["InvalidModel"]);
                }
            }
        }
    }
}
