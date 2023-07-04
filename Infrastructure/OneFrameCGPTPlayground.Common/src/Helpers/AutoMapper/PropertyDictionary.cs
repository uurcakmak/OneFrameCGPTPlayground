// <copyright file="PropertyDictionary.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Reflection;

namespace OneFrameCGPTPlayground.Common.Helpers.AutoMapper
{
    public class PropertyDictionary
    {
        public object Key { get; set; }

        public PropertyInfo Value { get; set; }
    }
}
