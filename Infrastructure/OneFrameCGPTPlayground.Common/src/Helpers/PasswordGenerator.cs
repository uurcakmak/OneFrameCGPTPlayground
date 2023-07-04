// <copyright file="PasswordGenerator.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace OneFrameCGPTPlayground.Common.Helpers
{
    public static class PasswordGenerator
    {
        /// <summary>Generates a Random Password
        /// respecting the given strength requirements.</summary>
        /// <param name="requiredLength">required password length.</param>
        /// <param name="requiredUniqueChars">required unique chars count.</param>
        /// <param name="requireDigit">require digit.</param>
        /// <param name="requireLowercase">require lowercase.</param>
        /// <param name="requireNonAlphanumeric">require nonalphanmeric.</param>
        /// <param name="requireUppercase">require uppercase.</param>
        /// <returns>generates a random password.</returns>
        public static string Generate(
            int requiredLength = 8,
            int requiredUniqueChars = 4,
            bool requireDigit = true,
            bool requireLowercase = true,
            bool requireNonAlphanumeric = true,
            bool requireUppercase = true)
        {
            var randomChars = new[]
            {
            "ABCDEFGHJKLMNPQRSTUVWXYZ",    // uppercase
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "123456789",                   // digits
            "!*@?",                         // non-alphanumeric
            };
            var rand = new Random(Guid.NewGuid().GetHashCode());
            var chars = new List<char>();

            if (requireUppercase)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[0][rand.Next(0, randomChars[0].Length)]);
            }

            if (requireLowercase)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[1][rand.Next(0, randomChars[1].Length)]);
            }

            if (requireDigit)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[2][rand.Next(0, randomChars[2].Length)]);
            }

            if (requireNonAlphanumeric)
            {
                chars.Insert(rand.Next(0, chars.Count), randomChars[3][rand.Next(0, randomChars[3].Length)]);
            }

            for (var i = chars.Count; i < requiredLength
                                      || chars.Distinct().Count() < requiredUniqueChars; i++)
            {
                var rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count), rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}