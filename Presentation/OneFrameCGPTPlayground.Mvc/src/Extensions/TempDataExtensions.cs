// <copyright file="TempDataExtensions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace OneFrameCGPTPlayground.Mvc.Extensions
{
    public static class TempDataExtensions
    {
        public static T Get<T>(this ITempDataDictionary tempData, string key)
            where T : class
        {
            object o = null;

            _ = tempData?.TryGetValue(key, out o);

            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value)
                    where T : class
        {
            if (tempData != null)
            {
                tempData[key] = JsonConvert.SerializeObject(value);
            }
        }
    }
}