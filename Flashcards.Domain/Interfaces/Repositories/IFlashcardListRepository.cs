using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IFlashcardListRepository
    {
        Task<IEnumerable<FlashcardList>> GetAllFlashcardListsByUserIdAsync(Guid UserId, CancellationToken cancellationToken = default);
        Task<FlashcardList?> GetByIdWithFlashcardsAsync(Guid flashcardListId, CancellationToken cancellationToken = default);

    }
}
