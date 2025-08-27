namespace BookingSystem.Extensions
{
    public static class MiddlewareExtensions
    {
        public static WebApplication UseSecurityMiddleware(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

        public static WebApplication UseCorsPolicy(this WebApplication app)
        {
            app.UseCors(policy =>
            {
                policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            return app;
        }
    }
}
