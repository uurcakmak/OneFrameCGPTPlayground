// <copyright file="CustomPasswordValidator.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Common.Extensions;
using KocSistem.OneFrame.I18N;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OneFrameCGPTPlayground.Infrastructure.Helpers.Identity
{
    /// <summary>
    /// Custom Password Validator.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    /// <seealso cref="IPasswordValidator{TUser}" />
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser>
        where TUser : class
    {
        private readonly IKsStringLocalizer<object> _localize;

        public CustomPasswordValidator(IKsStringLocalizer<object> localize)
        {
            _localize = localize;
        }

        /// <summary>
        /// Validates a password as an asynchronous operation.
        /// </summary>
        /// <param name="manager">The user manager to retrieve the <paramref name="user" /> properties from.</param>
        /// <param name="user">The user whose password should be validated.</param>
        /// <param name="password">The password supplied for validation.</param>
        /// <returns>
        /// The task object representing the asynchronous operation.
        /// </returns>
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            manager.ThrowIfNull(nameof(manager));

            var username = await manager.GetUserNameAsync(user).ConfigureAwait(false);

            var pass = password?.ToLower(new CultureInfo("en-US"));

            if (username.Equals(pass))
            {
                return IdentityResult.Failed(new IdentityError { Description = _localize["SameUserPass"], Code = "SameUserPass" });
            }

            var nonAllowedPasswordList = new List<string>
            {
                "123456",
                "password",
                "123456789",
                "12345",
                "12345678",
                "qwerty",
                "1234567",
                "111111",
                "1234567890",
                "123123",
                "abc123",
                "1234",
                "password1",
                "iloveyou",
                "1q2w3e4r",
                "000000",
                "qwerty123",
                "zaq12wsx",
                "dragon",
                "sunshine",
                "princess",
                "letmein",
                "654321",
                "monkey",
                "27653",
                "1qaz2wsx",
                "123321",
                "qwertyuiop",
                "superman",
                "asdfghjkl",
            };

            if (pass == null || nonAllowedPasswordList.Any(a => a == pass))
            {
                return IdentityResult.Failed(new IdentityError { Description = _localize["PasswordContainsPassword"], Code = "PasswordContainsPassword" });
            }

            return IdentityResult.Success;
        }
    }
}