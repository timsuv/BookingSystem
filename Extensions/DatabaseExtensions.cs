using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static WebApplication EnsureDatabaseCreated(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();
            context.Database.EnsureCreated();

            return app;
        }
    }
}
