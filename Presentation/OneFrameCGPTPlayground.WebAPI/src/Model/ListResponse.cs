// <copyright file="ListResponse.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.WebAPI.Model
{
    public class ListResponse<T>
    {
        public List<T> Items { get; set; }
    }
}
