using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleBookingSystem.Extensions.DatabaseExtensions
{
    public static class BuilderExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="marker"></param>
        /// <param name="conStr"></param>
        /// <typeparam name="TDbContext"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddSqlContext<TDbContext>(this IServiceCollection services, Type marker, string conStr)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options =>
            {
                options.UseSqlServer(conStr, sqlOptions =>
                {
                    sqlOptions.CommandTimeout(180);
                    sqlOptions.MigrationsAssembly(marker.Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, TimeSpan.FromSeconds(30), null);
                });

                options.EnableSensitiveDataLogging();
            });
            return services;
        }


    }
}
