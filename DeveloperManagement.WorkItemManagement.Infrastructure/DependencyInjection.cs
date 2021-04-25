using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.MimeType;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperManagement.WorkItemManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("DapperSettings").GetSection("ConnectionString").Value;
            services.AddSingleton<IDapperConnectionFactory>(new DapperConnectionFactory(connectionString));
            services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(
                provider.GetRequiredService<IDapperConnectionFactory>(),
                provider.GetRequiredService<IDomainEventService>(),
                provider.GetRequiredService<ICurrentUserService>(),
                provider.GetRequiredService<IDateTime>()));
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddSingleton<IMimeTypeMapper, MimeTypeMapper>();
            return services;
        }
    }
}