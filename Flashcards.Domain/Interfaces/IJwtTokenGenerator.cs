using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces
{
    public interface IJwtTokenGenerator 
    {
        string GenerateToken(User user);
    }
}
