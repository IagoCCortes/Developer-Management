using System;
using System.Reflection;
using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.MimeType;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Services;
using IntegrationEventLogDapper;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperManagement.WorkItemManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDapperConnectionFactory>(new DapperConnectionFactory(connectionString));
            services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(
                provider.GetRequiredService<IDapperConnectionFactory>(),
                provider.GetRequiredService<IDomainEventService>(),
                provider.GetRequiredService<ICurrentUserService>(),
                provider.GetRequiredService<IDateTime>()));
            services.AddTransient<IIntegrationEventLogService>(sp =>
                new IntegrationEventLogService(connectionString));
            services.AddScoped(sp => (IDomainUnitOfWork) sp.GetService(typeof(IUnitOfWork)));
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddSingleton<IMimeTypeMapper, MimeTypeMapper>();
            return services;
        }
    }
}