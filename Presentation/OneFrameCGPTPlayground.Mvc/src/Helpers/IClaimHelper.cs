// <copyright file="IClaimHelper.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.Mvc.Models.Account;

namespace OneFrameCGPTPlayground.Mvc.Helpers
{
    public interface IClaimHelper
    {
        Task BuildClaimsAndSignIn(LoginResponseViewModel loginResponse);
    }
}
