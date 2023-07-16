// <copyright file="ProfileController.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.DesignObjects.Services;
using Microsoft.AspNetCore.Authorization;
using OneFrameCGPTPlayground.Application.Abstractions.ApplicationSetting;
using OneFrameCGPTPlayground.Common.Constants;
using OneFrameCGPTPlayground.Mvc.Helpers;
using OneFrameCGPTPlayground.Mvc.Models.Profile;
using OneFrameCGPTPlayground.Mvc.Models.User;
using System.Globalization;
using System.Security.Claims;

namespace OneFrameCGPTPlayground.Mvc.Controllers
{
    [Route("profiles")]
    public class ProfileController : BaseController<ProfileController>
    {
        private readonly IConfiguration _configuration;
        private readonly IKsStringLocalizer<ProfileController> _localize;

        public ProfileController(IKsI18N i18N, IConfiguration configuration)
            : base(i18N)
        {
            _localize = i18N.GetLocalizer<ProfileController>();
            _configuration = configuration;
        }

        [HttpGet("change-password")]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return PartialView();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var profileResponse = await GetApiRequestAsync<ServiceResponse<ProfileModel>>(ApiEndpoints.UserGetUserInfo).ConfigureAwait(false);

            if (!profileResponse.IsSuccessful)
            {
                return ToastError(profileResponse.Error);
            }

            var profileModel = profileResponse.Result;

            if (profileModel.PhoneNumber != null && !profileModel.PhoneNumberConfirmed)
            {
                profileModel.PhoneNumberConfirmationUrl = $"{MvcEndpoints.ProfileConfirmPhoneNumberRoute}?phoneNumber={profileModel.PhoneNumber}";
            }

            var profilePhotoSizeResponse = await GetApiRequestAsync<ServiceResponse<ApplicationSettingDto>>(string.Format(CultureInfo.InvariantCulture, ApiEndpoints.ApplicationSettingGetByKey, ConfigurationConstant.IdentityProfilePhotoMaxSize)).ConfigureAwait(false);
            int profilePhotoSize = 0;
            if (profilePhotoSizeResponse.IsSuccessful)
            {
                profilePhotoSize = Convert.ToInt32(profilePhotoSizeResponse.Result.Value);
            }

            // ToDo: It should be moved as an Identity extension under the Common package
            var roleList = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(p => p.Value);

            profileModel.Roles = roleList != null ? string.Join(" | ", roleList) : string.Empty;

            profileModel.ProfilePhotoSize = profilePhotoSize;

            profileModel.TimeZoneList = await GetTimeZoneListAsync().ConfigureAwait(false);

            return View(profileModel);
        }

        private async Task<List<TimeZoneModel>> GetTimeZoneListAsync()
        {
            var response = await GetApiRequestAsync<ServiceResponse<List<TimeZoneModel>>>(ApiEndpoints.ConfigurationTimeZone).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return new List<TimeZoneModel>();
            }

            return response.Result;
        }

        [HttpPost("set-change-password")]
        [Authorize]
        public async Task<IActionResult> SetChangePasswordAsync(ChangePasswordModel model)
        {
            if (model.CurrentPassword.Equals(model.NewPassword, System.StringComparison.CurrentCulture))
            {
                return ToastError(_localize["NewPasswordCanNotBeSameWithTheCurrentOne"]);
            }

            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountChangePassword, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return ToastSuccessForRedirect(_localize["ChangePasswordSuccess"], "/");
        }

        [HttpPost("set-profile-info")]
        [Authorize]
        public async Task<IActionResult> SetProfileInfoAsync(ProfileModel model)
        {
            model.Email = User.Identity.GetUsername();
            var response = await PutApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountChangeProfileForUser, model).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            CookieHelper.Write(Response, CookieHelper.TimeZone, model.TimeZone, _configuration["CookieSettings:TimeZoneExpireDays"]);
            CookieHelper.Write(Response, CookieHelper.FullName, model.Name + ' ' + model.Surname, _configuration["CookieSettings:DefaultExpireDays"]);

            return ToastSuccess(_localize["UpdateUserSuccess"]);
        }

        [HttpPost("set-profile-photo")]
        [Authorize]
        public async Task<IActionResult> SetProfilePhotoAsync(string profilePhotoBase64)
        {
            var response = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountSetProfilePhoto, new ProfilePhotoModel { Photo = profilePhotoBase64 }).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.IsSuccessful);
        }

        [HttpGet("profile-photo")]
        [Authorize]
        public async Task<IActionResult> GetProfilePhotoAsync()
        {
            var response = await GetApiRequestAsync<ServiceResponse<ProfileModel>>(ApiEndpoints.UserGetUserInfo).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.Result.ProfilePhoto);
        }

        [HttpGet("confirm-phonenumber")]
        [Authorize]
        public async Task<IActionResult> GetConfirmationInfosAsync(string phoneNumber)
        {
            var confirmationResponse = await GetApiRequestAsync<ServiceResponse<ConfirmationCodeModel>>(ApiEndpoints.UserConfirmationCode, new { phoneNumber }).ConfigureAwait(false);

            if (!confirmationResponse.IsSuccessful)
            {
                return ToastError(confirmationResponse.Error);
            }

            if (!confirmationResponse.Result.IsSent)
            {
                var sendingSmsResponse = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.UserSendConfirmationCode, confirmationResponse.Result).ConfigureAwait(false);

                if (!sendingSmsResponse.IsSuccessful)
                {
                    return ToastErrorForRedirect(sendingSmsResponse.Error.Message, Url.Action("Index", "Home"));
                }
            }

            return View(new ConfirmationCodeViewModel
            {
                Id = confirmationResponse.Result.Id,
                PhoneNumber = confirmationResponse.Result.PhoneNumber,
                ExpiredDate = new DateTimeOffset(confirmationResponse.Result.ExpiredDate).ToUnixTimeSeconds(),
            });
        }

        [HttpPost("confirm-phonenumber")]
        [Authorize]
        public async Task<IActionResult> ConfirmPhoneNumberAsync(ConfirmationCodeViewModel model)
        {
            var confirmationResponse = await PostApiRequestAsync<ServiceResponse>(ApiEndpoints.UserConfirmationCode, model).ConfigureAwait(false);

            if (!confirmationResponse.IsSuccessful)
            {
                return ToastError(_localize["PhoneNumberConfirmationError"]);
            }

            return ToastSuccessForRedirect(_localize["UpdateUserSuccess"], Url.Action("Index", "Home"));
        }

        [HttpPost("delete-profile-photo")]
        [Authorize]
        public async Task<IActionResult> DeleteProfilePhotoAsync()
        {
            var response = await DeleteApiRequestAsync<ServiceResponse>(ApiEndpoints.AccountDeleteProfilePhoto).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                return ToastError(response.Error);
            }

            return Ok(response.IsSuccessful);
        }
    }
}