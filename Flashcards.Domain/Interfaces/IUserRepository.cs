using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellation = default);

    }
}
