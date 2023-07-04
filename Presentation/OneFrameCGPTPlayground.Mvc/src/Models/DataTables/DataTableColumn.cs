// <copyright file="DataTableColumn.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.DataTables
{
    /// <summary>
    /// Represents a datatable column.
    /// </summary>
    public class DataTableColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableColumn"/> class.
        /// Initialize a new instance of <see cref="DataTableColumn"/>.
        /// </summary>
        /// <param name="data">data.</param>
        /// <param name="name">name.</param>
        /// <param name="searchable">searchable.</param>
        /// <param name="orderable">orderable.</param>
        /// <param name="searchValue">searchValue.</param>
        /// <param name="regex">regex.</param>
        public DataTableColumn(string data, string name, bool searchable, bool orderable, string searchValue, bool regex)
        {
            Data = data;
            Name = name;
            Searchable = searchable;
            Orderable = orderable;
            SearchValue = searchValue;
            SearchRegEx = regex;
        }

        /// <summary>
        /// Column's data source, as defined by columns.data.
        /// </summary>
        public string Data { get; }

        /// <summary>
        /// Column's name, as defined by columns.name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Flag to indicate if this column is orderable (true) or not (false). This is controlled by columns.orderable.
        /// </summary>
        public bool Orderable { get; }

        /// <summary>
        /// Flag to indicate if this column is searchable (true) or not (false). This is controlled by columns.searchable.
        /// </summary>
        public bool Searchable { get; }

        /// <summary>
        /// Flag to indicate if the search term for this column should be treated as regular expression (true) or not (false).
        /// As with global search, normally server-side processing scripts will not perform regular expression searching for performance reasons on large data sets, but it is technically possible and at the discretion of your script.
        /// </summary>
        public bool SearchRegEx { get; }

        /// <summary>
        /// Search value to apply to this specific column.
        /// </summary>
        public string SearchValue { get; }
    }
}
