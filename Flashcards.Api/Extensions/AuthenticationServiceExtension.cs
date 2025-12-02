using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Flashcards.Api.Extensions
{
    public static class AuthenticationServiceExtension
    {
        public static IServiceCollection AddJwtAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            // Configure JWT-based authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Used to validate incoming requests
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Used when the system issues an authentication challenge (e.g., 401)
            })
            .AddJwtBearer(options =>
            {
                // Set up token validation parameters
                options.TokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuer = true, // Ensure the token was issued by a trusted source
                    ValidateAudience = true, // Ensure the token is intended for this API
                    ValidateLifetime = true, // Reject expired tokens
                    ValidateIssuerSigningKey = true, // Ensure the token was signed with the correct key
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key) // The key used to sign and validate the token

                };
            });

            return services;
        }
    }
}
