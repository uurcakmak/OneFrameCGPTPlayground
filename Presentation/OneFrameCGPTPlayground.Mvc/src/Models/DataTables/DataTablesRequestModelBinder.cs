// <copyright file="DataTablesRequestModelBinder.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OneFrameCGPTPlayground.Mvc.Models.DataTables
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "for grid")]
    internal class DataTablesRequestModelBinder : IModelBinder
    {
        /// <summary>
        /// Attempts to bind a model.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2008:Do not create tasks without passing a TaskScheduler", Justification = "origin from source code")]
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            return Task.Factory.StartNew(() =>
            {
                BindModel(bindingContext);
            });
        }

        /// <summary>
        /// Tries the get search.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns>Search.</returns>
        private static Search TryGetSearch(IValueProvider valueProvider)
        {
            if (TryParse(valueProvider.GetValue("search[value]"), out string searchValue) &&
                !string.IsNullOrEmpty(searchValue))
            {
                _ = TryParse(valueProvider.GetValue("search[regex]"), out bool regex);
                return new Search(searchValue, regex);
            }

            return null;
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <typeparam name="T">Genric type value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="result">The result.</param>
        /// <returns>bool.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "true/false result")]
        private static bool TryParse<T>(ValueProviderResult value, out T result)
        {
            result = default;

            try
            {
                result = (T)Convert.ChangeType(value.FirstValue, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="bindingContext">The binding context.</param>
        private static void BindModel(ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;

            var valueResultProvider = valueProvider.GetValue("draw");

            _ = TryParse(valueResultProvider, out int draw);
            _ = TryParse(valueProvider.GetValue("start"), out int start);
            _ = TryParse(valueProvider.GetValue("length"), out int length);

            var result = new DataTablesRequest(draw, start, length, TryGetSearch(valueProvider), TryGetOrders(valueProvider), TryGetColumns(valueProvider));
            bindingContext.Result = ModelBindingResult.Success(result);
        }

        /// <summary>
        /// Tries the get columns.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns>IEnumerable{DataTableColumn}.</returns>
        private static IEnumerable<DataTableColumn> TryGetColumns(IValueProvider valueProvider)
        {
            // columns[0][data]:name
            // columns[0][name]:
            // columns[0][searchable]:true
            // columns[0][orderable]:true
            // columns[0][search][value]:
            // columns[0][search][regex]:false
            var index = 0;
            var columns = new List<DataTableColumn>();

            // Count number of column
            do
            {
                if (valueProvider.GetValue($"columns[{index}][data]").FirstValue != null)
                {
                    _ = TryParse(valueProvider.GetValue($"columns[{index}][data]"), out string data);
                    _ = TryParse(valueProvider.GetValue($"columns[{index}][name]"), out string name);
                    _ = TryParse(valueProvider.GetValue($"columns[{index}][searchable]"), out bool searchable);
                    _ = TryParse(valueProvider.GetValue($"columns[{index}][orderable]"), out bool orderable);
                    _ = TryParse(valueProvider.GetValue($"columns[{index}][search][value]"), out string searchValue);
                    _ = TryParse(valueProvider.GetValue($"columns[{index}][search][regex]"), out bool searchRegEx);

                    columns.Add(new DataTableColumn(data, name, searchable, orderable, searchValue, searchRegEx));
                    index++;
                }
                else
                {
                    break;
                }
            }
            while (true);

            return columns;
        }

        /// <summary>
        /// Tries the get orders.
        /// </summary>
        /// <param name="valueProvider">The value provider.</param>
        /// <returns>IEnumerable{Order}.</returns>
        private static IEnumerable<Order> TryGetOrders(IValueProvider valueProvider)
        {
            // order[0][column]:0
            // order[0][dir]:asc
            var index = 0;
            var orders = new List<Order>();

            do
            {
                if (valueProvider.GetValue($"order[{index}][column]").FirstValue != null)
                {
                    _ = TryParse(valueProvider.GetValue($"order[{index}][column]"), out int column);
                    _ = TryParse(valueProvider.GetValue($"order[{index}][dir]"), out string dir);

                    orders.Add(new Order(column, dir));
                    index++;
                }
                else
                {
                    break;
                }
            }
            while (true);

            return orders;
        }
    }
}