using Flashcards.Application.Commons.OperationResult;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.DeleteFlashcard
{
    public class DeleteFlashcardCommandHandler : IRequestHandler<DeleteFlashcardCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Flashcard> _repository;

        public DeleteFlashcardCommandHandler(IGenericRepository<Flashcard> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<OperationResult<bool>> Handle(DeleteFlashcardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository
                    .GetByIdAsync(request.FlashcardId, cancellationToken);

                if (entity.FlashcardList.UserId != request.UserId)
                    return OperationResult<bool>.Failure("You are not authorized to delete this flashcard.");

                var deleted = await _repository.DeleteAsync(request.FlashcardId, cancellationToken);

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
