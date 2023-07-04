// <copyright file="DataTablesRequest.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace OneFrameCGPTPlayground.Mvc.Models.DataTables
{
    /// <summary>
    /// Represents a datatable request for serverSide request.
    /// </summary>
    public class DataTablesRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTablesRequest"/> class.
        /// </summary>
        /// <param name="draw">The draw.</param>
        /// <param name="start">The start.</param>
        /// <param name="length">The length.</param>
        /// <param name="search">The search.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="columns">The columns.</param>
        public DataTablesRequest(int draw, int start, int length, Search search, IEnumerable<Order> orders, IEnumerable<DataTableColumn> columns)
        {
            Draw = draw;
            Start = start;
            Length = length;
            Search = search;
            Orders = orders;
            Columns = columns;
        }

        /// <summary>
        /// Column's data source.
        /// </summary>
        public IEnumerable<DataTableColumn> Columns { get; }

        /// <summary>
        /// Draw counter. This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables (Ajax requests are asynchronous and thus can return out of sequence). This is used as part of the draw return parameter (see below).
        /// </summary>
        public int Draw { get; }

        /// <summary>
        /// Number of records that the table can display in the current draw. It is expected that the number of records returned will be equal to this number, unless the server has fewer records to return. Note that this can be -1 to indicate that all records should be returned (although that negates any benefits of server-side processing!).
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Length { get; }

        /// <summary>
        /// Column to which ordering should be applied. This is an index reference to the columns array of information that is also submitted to the server.
        /// </summary>
        public IEnumerable<Order> Orders { get; }

        /// <summary>
        /// Global search value. To be applied to all columns which have searchable as true.
        /// </summary>
        public Search Search { get; }

        /// <summary>
        /// Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        /// </summary>
        public int Start { get; }
    }
}
