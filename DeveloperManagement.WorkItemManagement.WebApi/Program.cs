using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Seed;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeveloperManagement.WorkItemManagement.WebApi
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
                    var connectionFactory = services.GetRequiredService<IDapperConnectionFactory>();
                    var applicationSeeder = new ApplicationSeeder(connectionFactory);
                    await applicationSeeder.SeedDatabaseAsync();
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