using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace SimpleBookingSystem.Extensions.MigrationExtensions
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder Migrate(this IApplicationBuilder app, MigrationUpdates migrationUpdate = MigrationUpdates.MigrateUp, long migrationVersion = 0)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<IStartup>>();
            try
            {
                var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
                switch (migrationUpdate)
                {
                    case MigrationUpdates.MigrateUp:
                        if (migrationVersion != 0)
                        {
                            runner.MigrateUp(migrationVersion);
                        }
                        else
                        {
                            runner.MigrateUp();
                        }
                        logger.LogInformation("UpMigration finished successfully.");
                        break;

                    case MigrationUpdates.MigrateDown:
                        runner.MigrateDown(migrationVersion);
                        logger.LogInformation($"DownMigration version:{migrationVersion} finished successfully.");
                        break;

                    case MigrationUpdates.Rollback:
                        runner.RollbackToVersion(migrationVersion);
                        logger.LogInformation($"RollbackToVersion {migrationVersion} finished successfully.");
                        break;
                }
            }
            catch (SqlException odbcEx)
            {
                if (odbcEx.Message.ToLower().Contains("Login failed for user".ToLower()))
                {
                    logger.LogError(odbcEx, "Migration: Ex number: ??");
                }
                else
                {
                    logger.LogError(odbcEx, "Migration: failed");
                }
                // Handle more specific SqlException exception here.  
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migration: failed");
            }
            return app;
        }
    }
}
