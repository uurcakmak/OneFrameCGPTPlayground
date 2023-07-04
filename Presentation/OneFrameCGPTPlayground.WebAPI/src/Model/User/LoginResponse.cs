// <copyright file="LoginResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.ClaimHelper;

namespace OneFrameCGPTPlayground.WebAPI.Model.User
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public IList<ClaimResponse> Claims { get; set; }
    }
}