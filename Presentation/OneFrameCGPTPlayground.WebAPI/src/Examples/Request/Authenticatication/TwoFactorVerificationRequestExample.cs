// <copyright file="TwoFactorVerificationRequestExample.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using OneFrameCGPTPlayground.WebAPI.Model.User;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;

namespace OneFrameCGPTPlayground.WebAPI.Examples.Request
{
    [ExcludeFromCodeCoverage(Justification = "not necessary")]
    [SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for SwaggerUI")]
    internal class TwoFactorVerificationRequestExample : IExamplesProvider<TwoFactorVerificationRequest>
    {
        public TwoFactorVerificationRequest GetExamples()
        {
            return new TwoFactorVerificationRequest
            {
                UserName = "ghostbusters@kocsistem.com.tr",
                VerificationCode = "654321",
                VerificationType = Common.Enums.Login2Fa.Authenticator,
            };
        }
    }
}