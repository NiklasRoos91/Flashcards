using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Interfaces.Services;
using Flashcards.Infrastructure.Presistence;
using Flashcards.Infrastructure.Repositories;
using Flashcards.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flashcards.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with SQL Server
            services.AddDbContext<FlashcardsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FlashcardsApiDb")));

            // Register repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFlashcardListRepository, FlashcardListRepository>();
            services.AddScoped<IFlashcardRepository, FlashcardRepository>();

            return services;
        }
    }
}
