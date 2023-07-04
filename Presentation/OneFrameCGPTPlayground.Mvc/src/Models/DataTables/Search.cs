// <copyright file="Search.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.DataTables
{
    /// <summary>
    /// Represents the search options.
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Search"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="regex">if set to <c>true</c> [regex].</param>
        public Search(string value, bool regex)
        {
            Value = value;
            Regex = regex;
        }

        /// <summary>
        /// true if the global filter should be treated as a regular expression for advanced searching, false otherwise. Note that normally server-side processing scripts will not perform regular expression searching for performance reasons on large data sets, but it is technically possible and at the discretion of your script.
        /// search[regex].
        /// </summary>
        public bool Regex { get; }

        /// <summary>
        /// Global search value. To be applied to all columns which have searchable as true.
        /// search[value].
        /// </summary>
        public string Value { get; }
    }
}
