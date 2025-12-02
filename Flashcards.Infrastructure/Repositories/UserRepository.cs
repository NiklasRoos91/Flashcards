using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FlashcardsDbContext _context;

        public UserRepository(FlashcardsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
