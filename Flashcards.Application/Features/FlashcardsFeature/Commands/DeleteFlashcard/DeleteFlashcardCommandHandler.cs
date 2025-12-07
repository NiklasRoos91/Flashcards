using Flashcards.Application.Commons.OperationResult;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.DeleteFlashcard
{
    public class DeleteFlashcardCommandHandler : IRequestHandler<DeleteFlashcardCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Flashcard> _genericRepository;
        private readonly IFlashcardRepository _flashcardRepository; 

        public DeleteFlashcardCommandHandler(IGenericRepository<Flashcard> genericRepository, IFlashcardRepository flashcardRepository)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            _flashcardRepository = flashcardRepository ?? throw new ArgumentNullException(nameof(flashcardRepository));
        }

        public async Task<OperationResult<bool>> Handle(DeleteFlashcardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flashcard = await _flashcardRepository.GetByIdWithListAsync(request.FlashcardId, cancellationToken);
                if (flashcard.FlashcardList.UserId != request.UserId)
                    return OperationResult<bool>.Failure("You are not authorized to delete this flashcard.");

                var deleted = await _genericRepository.DeleteAsync(request.FlashcardId, cancellationToken);

                return OperationResult<bool>.Success(deleted);
            }
            catch (KeyNotFoundException)
            {
                return OperationResult<bool>.Failure("Flashcard not found.");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the flashcard: {ex.Message}");
            }
        }
    }
}
