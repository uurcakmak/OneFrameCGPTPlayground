// <copyright file="JsonDataTableObject{T}.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
namespace OneFrameCGPTPlayground.Mvc.Models.Other
{
    public class JsonDataTableObject<T>
    {
        public System.Collections.Generic.IList<T> Data { get; set; }

        public int ITotalDisplayRecords { get; set; }

        public int RecordsTotal { get; set; }
    }
}