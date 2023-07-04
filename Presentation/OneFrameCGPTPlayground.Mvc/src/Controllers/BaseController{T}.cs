// <copyright file="BaseController{T}.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Net;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    public abstract class BaseController<T> : BaseController
    {
        private readonly IKsStringLocalizer<T> _localize;

        protected BaseController(IKsI18N i18N)
        {
            _localize = i18N.GetLocalizer<T>();
        }

        protected IActionResult ActionResult(HttpResponseMessage response, string notFoundResourceKey = "NotFound")
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    return ToastError(_localize[notFoundResourceKey]);

                case HttpStatusCode.NotFound:
                    return ToastError(_localize[notFoundResourceKey]);

                case HttpStatusCode.Unauthorized:
                    return ToastError(_localize["Unauthorized"]);

                case HttpStatusCode.Forbidden:
                    return RedirectToAction("AccessDenied", "Account");

                default:
                    {
                        var stateInfo = response.Content.ReadAsStringAsync().Result;
                        return ToastError(GetApiResponseMessage(stateInfo));
                    }
            }
        }
    }
}