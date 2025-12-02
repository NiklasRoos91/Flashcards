using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces
{
    public interface IFlashcardListRepository
    {
        Task<IEnumerable<FlashcardList>> GetAllFlashcardListsByUserIdAsync(Guid UserId, CancellationToken cancellationToken = default);
    }
}
