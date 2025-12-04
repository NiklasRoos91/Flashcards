using Flashcards.Api.Extensions;
using Flashcards.Application.Extensions.ServiceCollectionExtensions;
using Flashcards.Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace Flashcards.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add infrastructure and application services
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            // Add JWT authentication setup
            builder.Services.AddJwtAuthenticationService(builder.Configuration);


            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Ensures enums are serialized as strings instead of numbers
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                });

            // Add Swagger and include JWT Bearer configuration
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                // Add Swagger and include JWT Bearer configuration
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    // Security definition for JWT Bearer token support in Swagger
                    Description = "JWT Authorization header using the Bearer scheme. Enter only your token.", 
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Apply the security requirement globally
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            // Configure CORS policy to allow requests from Vite frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Apply CORS before authentication
            app.UseCors("AllowFrontend");

            app.UseAuthentication();

            app.UseAuthorization(); 


            app.MapControllers();

            app.Run();
        }
    }
}
