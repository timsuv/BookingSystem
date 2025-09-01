using BookingSystem.Repositories.BookingRepository;
using BookingSystem.Repositories.CustomerRepository;
using BookingSystem.Repositories.MenuRepository;
using BookingSystem.Repositories.TableRepository;
using BookingSystem.Services;

namespace BookingSystem.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseServices(configuration)
                   .AddJwtAuthentication(configuration)
                   .AddAuthServices()
                   .AddBusinessServices()
                   .AddSwaggerDocumentation();

            return services;
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            //repos
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ITableRepositiory, TableRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            //services
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<ICustomerSevice, CustomerService>();
            services.AddScoped<IMenuService, MenuService>();

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}

