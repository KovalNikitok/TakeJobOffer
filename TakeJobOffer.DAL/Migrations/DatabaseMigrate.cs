using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TakeJobOffer.DAL.Migrations
{
    public static class DatabaseMigrate
    {
        public static IApplicationBuilder MigrateDatabase<TDbContext, TILoggerProvider>(this IApplicationBuilder app)
            where TDbContext : DbContext
            where TILoggerProvider : class
        {
            using var scope = app.ApplicationServices.CreateScope();

            var services = scope.ServiceProvider;

            DbContext dbContext = services.GetRequiredService<TDbContext>();
            ILogger logger = services.GetRequiredService<ILogger<TILoggerProvider>>();

            while (!dbContext.Database.CanConnect())
            {
                logger.LogInformation("Database not ready yet; waiting...");
                Thread.Sleep(1000);
            }

            try
            {
                dbContext.Database.Migrate();
                logger.LogInformation("Database migrated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
            }

            return app;
        }
    }
}
