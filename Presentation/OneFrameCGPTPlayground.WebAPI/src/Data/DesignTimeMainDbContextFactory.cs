// <copyright file="DesignTimeMainDbContextFactory.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OneFrameCGPTPlayground.Persistence;
using OneFrameCGPTPlayground.Persistence.Contexts.Main;

namespace OneFrameCGPTPlayground.WebAPI.Data
{
    /// <summary>
    /// DesignTimeMainDbContextFactory.
    /// </summary>
    public class DesignTimeMainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// MainDb Context.
        /// </returns>
        public MainDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<MainDbContext>();

            builder.ConfigureDatabase(
                configuration["Data:MainDbContext:ConnectionString"],
                configuration["Data:MainDbContext:MigrationsAssembly"],
                int.Parse(configuration["Data:MainDbContext:DefaultTimeOut"]));

            return new MainDbContext(builder.Options);
        }
    }
}
