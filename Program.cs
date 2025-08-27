
using BookingSystem.Extensions;

namespace BookingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add all services
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddCorsPolicy();

            var app = builder.Build();

            // Configure pipeline
            app.EnsureDatabaseCreated()
               .UseSwaggerDocumentation()
               .UseCorsPolicy()
               .UseSecurityMiddleware()
               .MapAllEndpoints();

            app.Run();
        }
    }
}
