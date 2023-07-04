// <copyright file="Program.cs" company="KocSistem">
// Copyright (c) KocSistem. All rights reserved.
// Licensed under the Proprietary license. See LICENSE file in the project root for full license information.
// </copyright>

using NLog.Web;

namespace OneFrameCGPTPlayground.Mvc
{
    public static class Program
    {
        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            _ = webBuilder.ConfigureKestrel(serverOptions =>
              {
                  // Set properties and call methods on options
              })
            .UseStartup<Startup>()
            .UseNLog();
        });

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
    }
}