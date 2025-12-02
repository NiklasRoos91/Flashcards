using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces
{
    public interface IFlashcardRepository
    {
        Task<Flashcard?> GetRandomFlashcardByFlashcardListIdAsync(Guid flashcardListId, CancellationToken cancellationToken = default);
    }
}
