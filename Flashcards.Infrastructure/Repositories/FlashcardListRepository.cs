using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Infrastructure.Repositories
{
    public class FlashcardListRepository : IFlashcardListRepository
    {
        private readonly FlashcardsDbContext _context;

        public FlashcardListRepository(FlashcardsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<FlashcardList>> GetAllFlashcardListsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.FlashcardLists
                .Include(fl => fl.Flashcards)
                .Where(fl => fl.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}