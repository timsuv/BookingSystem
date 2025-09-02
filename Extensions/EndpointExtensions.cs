
namespace BookingSystem.Extensions
{
    public static class EndpointExtensions
    {
        public static WebApplication MapAllEndpoints(this WebApplication app)
        {
            app.MapControllers();
            return app;
        }

    }
}
