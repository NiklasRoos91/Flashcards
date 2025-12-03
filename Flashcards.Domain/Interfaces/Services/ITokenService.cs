using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Services
{
    public interface ITokenService 
    {
        string GenerateToken(User user);
    }
}
