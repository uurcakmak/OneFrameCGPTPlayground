// <copyright file="DataTablesRequestAttribute.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.DataTables
{
    /// <summary>
    /// Data Table Request Attribute.
    /// </summary>
    /// <seealso cref="ModelBinderAttribute" />
    public class DataTablesRequestAttribute : ModelBinderAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTablesRequestAttribute"/> class.
        /// Initialize a new instance of <see cref="DataTablesRequestAttribute"/>.
        /// </summary>
        public DataTablesRequestAttribute()
            : base(typeof(DataTablesRequestModelBinder))
        {
        }
    }
}