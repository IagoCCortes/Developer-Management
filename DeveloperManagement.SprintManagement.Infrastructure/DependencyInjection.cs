using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using DeveloperManagement.SprintManagement.Infrastructure.Persistence;
using DeveloperManagement.SprintManagement.Infrastructure.Persistence.Repositories;
using DeveloperManagement.SprintManagement.Infrastructure.Persistence.Repositories.SprintAR;
using DeveloperManagement.SprintManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperManagement.SprintManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetSection("EfSettings").GetSection("ConnectionString").Value;
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }, ServiceLifetime.Scoped);

            services.AddScoped<ISprintRepository, SprintRepository>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}