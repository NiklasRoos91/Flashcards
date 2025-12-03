using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure.Repositories
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly FlashcardsDbContext _dbContext;

        public FlashcardRepository(FlashcardsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<Flashcard?> GetRandomFlashcardByFlashcardListIdAsync(Guid flashcardListId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Flashcards
                .Where(f => f.FlashcardListId == flashcardListId)
                .OrderBy(_ => Guid.NewGuid()) // Random order
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
