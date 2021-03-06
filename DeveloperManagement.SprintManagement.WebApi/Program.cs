using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Infrastructure.Persistence;
using DeveloperManagement.SprintManagement.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeveloperManagement.SprintManagement.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
            
                    if (context.Database.IsMySql())
                    {
                        await context.Database.MigrateAsync();
                    }
                    await ApplicationDbContextSeed.SeedDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            
                    throw;
                }
            }

            await host.RunAsync();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}