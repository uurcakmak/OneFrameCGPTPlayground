// <copyright file="BaseController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.DesignObjects.Models;
using KocSistem.OneFrame.DesignObjects.Services;
using Newtonsoft.Json.Linq;
using OneFrameCGPTPlayground.Infrastructure.Helpers.Client;
using OneFrameCGPTPlayground.Mvc.Extensions;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.Configuration;
using OneFrameCGPTPlayground.Mvc.Models.DataTables;
using OneFrameCGPTPlayground.Mvc.Models.Other;
using OneFrameCGPTPlayground.Mvc.Models.Paging;
using OneFrameCGPTPlayground.Mvc.Toast;
using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    public abstract class BaseController : Controller
    {
        private IClientProxy ProxyHelper => HttpContext.RequestServices.GetService<IClientProxy>();

        public async Task<ConfigurationModel> GetConfigurationsAsync(List<string> keyList)
        {
            var configurationResponse = await PostApiRequestAsync<ServiceResponse<ConfigurationModel>>(ApiEndpoints.ConfigurationMvcUi, keyList, false).ConfigureAwait(false);
            var configuration = configurationResponse.Result;
            return configuration;
        }

        public JsonResult ToastError(ErrorInfo errorInfo)
        {
            return Json(new { Type = ToastType.Error.ToString(), Message = GetErrorInfoMessages(errorInfo), IsRedirect = false });
        }

        public JsonResult ToastError(ErrorInfo errorInfo, string message)
        {
            var result = message;
            if (errorInfo != null && !string.IsNullOrEmpty(errorInfo.Message))
            {
                result += $"<br/>{errorInfo.Message}";
            }

            return Json(new { Type = ToastType.Error.ToString(), Message = result, IsRedirect = false });
        }

        public JsonResult ToastError(string message)
        {
            return Json(new { Type = ToastType.Error.ToString(), Message = message, IsRedirect = false });
        }

        public JsonResult ToastError(string message, List<ValidationResult> validationErrors)
        {
            var result = message;
            if (validationErrors != null && validationErrors.Count > 0)
            {
                foreach (var errorMessage in validationErrors)
                {
                    result += $"<br/>{errorMessage.ErrorMessage}";
                }
            }

            return Json(new { Type = ToastType.Error.ToString(), Message = result, IsRedirect = false });
        }

        protected static string GetApiResponseMessage(string result)
        {
            var jObject = JObject.Parse(result);

            // .. - recursive descent
            var classNameTokens = jObject.SelectTokens("..message");
            var values = classNameTokens.Select(x => (x as JValue).Value);
            var message = string.Join(", ", values.ToArray());

            return message;
        }

        protected static PagedRequest GetPagedRequest(DataTablesRequest pagedRequestModel)
        {
            var pagedRequest = new PagedRequest()
            {
                PageIndex = pagedRequestModel.Start / pagedRequestModel.Length,
                PageSize = pagedRequestModel.Length,
            };

            if (pagedRequestModel.Orders != null)
            {
                foreach (var order in pagedRequestModel.Orders)
                {
                    pagedRequest.Orders.Add(new PagedRequestOrder
                    {
                        ColumnName = pagedRequestModel.Columns.ElementAt(order.Column).Data,
                        DirectionDesc = order.Dir != "asc",
                    });
                }
            }

            return pagedRequest;
        }

        protected static dynamic JsonDataTable<T>(PagedResult<T> data)
        {
            return new JsonDataTableObject<T> { RecordsTotal = data.TotalCount, ITotalDisplayRecords = data.TotalCount, Data = data.Items };
        }

        protected async Task<T> DeleteApiRequestAsync<T>(string endpoint, bool addAuthorization = true)
        {
            return await ProxyHelper.DeleteApiRequest<T>(endpoint, addAuthorization, false, false).ConfigureAwait(false);
        }

        protected async Task<T> DeleteApiRequestWithAllHeadersAsync<T>(string endpoint)
        {
            return await ProxyHelper.DeleteApiRequest<T>(endpoint, false, false, true).ConfigureAwait(false);
        }

        protected async Task<T> DeleteApiRequestWithCookiesAsync<T>(string endpoint, bool addAuthorization = true)
        {
            return await ProxyHelper.DeleteApiRequest<T>(endpoint, addAuthorization, true, false).ConfigureAwait(false);
        }

        protected async Task<T> GetApiRequestAsync<T>(string endpoint, object queryParams = null, bool addAuthorization = true)
        {
            return await ProxyHelper.GetApiRequest<T>(endpoint, queryParams, addAuthorization, false, false).ConfigureAwait(false);
        }

        protected async Task<T> GetApiRequestWithAllHeadersAsync<T>(string endpoint, object queryParams = null)
        {
            return await ProxyHelper.GetApiRequest<T>(endpoint, queryParams, false, false, true).ConfigureAwait(false);
        }

        protected async Task<T> GetApiRequestWithCookiesAsync<T>(string endpoint, object queryParams = null, bool addAuthorization = true)
        {
            return await ProxyHelper.GetApiRequest<T>(endpoint, queryParams, addAuthorization, true, false).ConfigureAwait(false);
        }

        protected async Task<T> PostApiRequestAsync<T>(string endpoint, object model, bool addAuthorization = true)
        {
            return await ProxyHelper.PostApiRequest<T>(endpoint, model, addAuthorization, false, false).ConfigureAwait(false);
        }

        protected async Task<T> PostApiRequestWithAllHeadersAsync<T>(string endpoint, object model)
        {
            return await ProxyHelper.PostApiRequest<T>(endpoint, model, false, false, true).ConfigureAwait(false);
        }

        protected async Task<T> PostApiRequestWithCookiesAsync<T>(string endpoint, object model, bool addAuthorization = true)
        {
            return await ProxyHelper.PostApiRequest<T>(endpoint, model, addAuthorization, true, false).ConfigureAwait(false);
        }

        protected async Task<T> PutApiRequestAsync<T>(string endpoint, object model, bool addAuthorization = true)
        {
            return await ProxyHelper.PutApiRequest<T>(endpoint, model, addAuthorization, false, false).ConfigureAwait(false);
        }

        protected async Task<T> PutApiRequestWithAllHeadersAsync<T>(string endpoint, object model)
        {
            return await ProxyHelper.PutApiRequest<T>(endpoint, model, false, false, true).ConfigureAwait(false);
        }

        protected async Task<T> PutApiRequestWithCookiesAsync<T>(string endpoint, object model, bool addAuthorization = true)
        {
            return await ProxyHelper.PutApiRequest<T>(endpoint, model, addAuthorization, true, false).ConfigureAwait(false);
        }

        protected JsonResult Toast(ToastModel toast)
        {
            return Json(new { Type = toast.ToastType.ToString(), toast.Message, IsRedirect = false });
        }

        protected JsonResult ToastErrorForRedirect(string message, string redirectUrl)
        {
            var toast = new ToastModel
            {
                ToastType = ToastType.Error,
                Message = message,
            };

            TempData.Put("Notifications", toast);

            return Json(new { RedirectUrl = redirectUrl, IsRedirect = true });
        }

        protected JsonResult ToastForRedirect(ToastModel toast, string redirectUrl)
        {
            TempData.Put("Notifications", toast);

            return Json(new { RedirectUrl = redirectUrl, IsRedirect = true });
        }

        protected JsonResult ToastSuccess(string message)
        {
            return Json(new { Type = ToastType.Success.ToString(), Message = message, IsRedirect = false });
        }

        protected JsonResult ToastSuccessForRedirect(string message, string redirectUrl)
        {
            var toast = new ToastModel
            {
                ToastType = ToastType.Success,
                Message = message,
            };

            TempData.Put("Notifications", toast);

            return Json(new { RedirectUrl = redirectUrl, IsRedirect = true });
        }

        private static string GetErrorInfoMessages(ErrorInfo errorInfo)
        {
            var result = string.Empty;
            if (errorInfo != null && !string.IsNullOrEmpty(errorInfo.Message))
            {
                result += $"{errorInfo.Message}";
            }

            return result;
        }
    }
}