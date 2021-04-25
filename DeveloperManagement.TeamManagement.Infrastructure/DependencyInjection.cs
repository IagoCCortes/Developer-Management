namespace DeveloperManagement.TeamManagement.Infrastructure
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

            services.AddScoped<IWorkItemRepository, WorkItemRepository>();
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}