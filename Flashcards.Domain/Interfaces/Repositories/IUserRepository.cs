using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellation = default);

    }
}
