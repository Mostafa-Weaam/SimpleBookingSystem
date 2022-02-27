using AutoMapper;
using FluentMigrator.Runner;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Extensions.DatabaseExtensions;
using SimpleBookingSystem.Extensions.MigrationExtensions;
using SimpleBookingSystemService.Infrastructure;
using SimpleBookingSystemService.Infrastructure.Services;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleBookingSystem
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
            services.AddControllers();
            services.AddSqlContext<SimpleBookingSystemContext>(typeof(Startup), Configuration.GetConnectionString("PO_Booking"));
            services.AddAutoMapper(Assembly.GetEntryAssembly(), typeof(Startup).Assembly);
            services.AddMediatR(Assembly.GetEntryAssembly(), typeof(Startup).Assembly);

            services
            .AddFluentMigratorCore()
            .ConfigureRunner(runBuilder =>
              runBuilder
                  .AddSqlServer()
                  .WithGlobalConnectionString(Configuration.GetConnectionString("PO_Booking"))
                  .ScanIn(typeof(Startup).Assembly).For.Migrations());

            services.Configure<SwaggerOptions>(Configuration.GetSection("Swagger"));
            services.Configure<SwaggerGenOptions>(Configuration.GetSection("SwaggerGen"));
            services.Configure<SwaggerUIOptions>(Configuration.GetSection("SwaggerUI"));

            services.AddSwaggerGen();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IValidationService, ValidationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var serviceScope = serviceScopeFactory.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<SimpleBookingSystemContext>();

            if (dbContext.Database.IsSqlServer())
            {
                app.Migrate();
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
