using BookingSystem.DTOs;
using BookingSystem.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var authGroup = app.MapGroup("/api/auth").WithTags("Authentication");

            authGroup.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
            {
                if (!ModelState.IsValid(request))
                    return Results.BadRequest("Invalid request data");

                var result = await authService.Login(request);

                if (result == null)
                    return Results.Unauthorized();

                return Results.Ok(result);




            })
                .WithName("Login")
                .WithSummary("Login as admin");

            authGroup.MapPost("/register", async (RegisterRequest request, IAuthService authService) =>
            {
                if (!ModelState.IsValid(request))
                    return Results.BadRequest("Invalid request data");

                

                var success = await authService.Register(request);

                if (!success)
                    return Results.BadRequest("Failed to create administrator");

                return Results.Ok(new { message = "Administrator created successfully" });
            })
            .WithName("Register")
            .WithSummary("Register new administrator");

        }
       
    }

    public static class ModelState
    {
        public static bool IsValid(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }
    }

}
