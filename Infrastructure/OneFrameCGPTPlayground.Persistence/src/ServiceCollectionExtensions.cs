// <copyright file="ServiceCollectionExtensions.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using KocSistem.OneFrame.Data.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneFrameCGPTPlayground.Persistence.Contexts.Main;

namespace OneFrameCGPTPlayground.Persistence
{
    /// <summary>
    /// ServiceCollectionExtensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the persistence infrastructure.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="configuration">The configuration.</param>
        public static void AddPersistenceInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRelationalData<MainDbContext>((options) =>
            {
                options.ConfigureDatabase(configuration["Data:MainDbContext:ConnectionString"], configuration["Data:MainDbContext:MigrationsAssembly"], int.Parse(configuration["Data:MainDbContext:DefaultTimeOut"]));
            });
        }

        /// <summary>
        /// Configures the database.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="migrationAssembly">The migration assembly.</param>
        public static void ConfigureDatabase(this DbContextOptionsBuilder builder, string connectionString, string migrationAssembly, int defaultTimeOut)
        {
            _ = builder.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly(migrationAssembly);
                sqlServerOptions.CommandTimeout(defaultTimeOut);
            });
        }
    }
}
