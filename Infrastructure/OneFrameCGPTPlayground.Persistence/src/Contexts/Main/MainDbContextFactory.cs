// -----------------------------------------------------------------------
// <copyright file="MainDbContextFactory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OneFrameCGPTPlayground.Persistence.Contexts.Main
{
    /// <summary>
    /// MainDbContextFactory.
    /// </summary>
    public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of MainDbContext.
        /// </returns>
        public MainDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
            return new MainDbContext(optionsBuilder.Options);
        }
    }
}