// <copyright file="ICaptchaValidator.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Infrastructure.Helpers.Captcha
{
    public interface ICaptchaValidator
    {
        Task<bool> IsCaptchaPassedAsync(string token);

        void UpdateSecretKey(string key);
    }
}
