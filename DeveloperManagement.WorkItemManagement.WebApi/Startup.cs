using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Application;
using DeveloperManagement.WorkItemManagement.Infrastructure;
using DeveloperManagement.WorkItemManagement.WebApi.Configurations;
using DeveloperManagement.WorkItemManagement.WebApi.Filters;
using DeveloperManagement.WorkItemManagement.WebApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DeveloperManagement.WorkItemManagement.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddHealthChecks();

            services.AddControllers(options =>
                {
                    options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
                    options.Filters.Add<ApiExceptionFilterAttribute>();
                })
                .AddFluentValidation();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = typeof(Startup).Assembly.FullName,
                    Description = "Work Items - WebApi",
                    Contact = new OpenApiContact
                    {
                        Name = "Iago",
                        Url = new Uri("https://www.google.com"),
                    }
                });

                s.OperationFilter<HeaderFilter>();
            });

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeveloperManagement.WorkItemManagement.WebApi v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}