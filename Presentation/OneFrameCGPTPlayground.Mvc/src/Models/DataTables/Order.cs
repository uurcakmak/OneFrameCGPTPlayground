// <copyright file="Order.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.DataTables
{
    /// <summary>
    /// Represents order in <see cref="DataTablesRequest"/>.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="dir">The dir.</param>
        public Order(int column, string dir)
        {
            Column = column;
            Dir = dir;
        }

        /// <summary>
        /// Column to which ordering should be applied. This is an index reference to the columns array of information that is also submitted to the server.
        /// order[i][column].
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Ordering direction for this column. It will be asc or desc to indicate ascending ordering or descending ordering, respectively.
        /// order[i][dir].
        /// </summary>
        public string Dir { get; }
    }
}
