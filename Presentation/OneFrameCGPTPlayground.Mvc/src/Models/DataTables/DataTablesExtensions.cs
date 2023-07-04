// <copyright file="DataTablesExtensions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

namespace OneFrameCGPTPlayground.Mvc.Models.DataTables
{
    /// <summary>
    /// Provides extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class DataTablesExtensions
    {
        /// <summary>
        /// Converts to datatables response.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="request">The request.</param>
        /// <returns>DataTablesResponse{T}.</returns>
        public static DataTablesResponse<T> ToDataTablesResponse<T>(this IEnumerable<T> collection, DataTablesRequest request)
        {
            var response = new DataTablesResponse<T>
            {
                Draw = request.Draw,
                RecordsTotal = collection.Count(),
                RecordsFiltered = collection.Count(),
                Data = collection
            };
            return response;
        }

        /// <summary>
        /// Converts to datatables response.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="request">The request.</param>
        /// <param name="recordsTotal">The records total.</param>
        /// <returns>DataTablesResponse{T}.</returns>
        public static DataTablesResponse<T> ToDataTablesResponse<T>(this IEnumerable<T> collection, DataTablesRequest request, int recordsTotal)
        {
            var response = new DataTablesResponse<T>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsTotal,
                Data = collection
            };
            return response;
        }

        /// <summary>
        /// Gets a <see cref="DataTablesResponse{T}"/> from collection/request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="request"></param>
        /// <param name="recordsTotal">Number of records before filtered.</param>
        /// <param name="recordsFiltered">Number of records after filtered.</param>
        /// <returns></returns>
        public static DataTablesResponse<T> ToDataTablesResponse<T>(this IEnumerable<T> collection, DataTablesRequest request, int recordsTotal, int recordsFiltered)
        {
            var response = new DataTablesResponse<T>
            {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = collection
            };
            return response;
        }
    }
}