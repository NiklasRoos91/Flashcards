using Flashcards.Application.Commons.OperationResult;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using MediatR;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Commands.DeleteFlashcardList
{
    public class DeleteFlashcardListCommandHandler : IRequestHandler<DeleteFlashcardListCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<FlashcardList> _repository;

        public DeleteFlashcardListCommandHandler(IGenericRepository<FlashcardList> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<OperationResult<bool>> Handle(DeleteFlashcardListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(request.FlashcardListId, cancellationToken);

                if (entity == null)
                    return OperationResult<bool>.Failure("Flashcard list not found.");

                if (entity.UserId != request.UserId)
                    return OperationResult<bool>.Failure("You do not have permission to delete this list.");

                var success = await _repository.DeleteAsync(request.FlashcardListId, cancellationToken);

                return success ? OperationResult<bool>.Success(true) : OperationResult<bool>.Failure("Deletion failed.");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"Error deleting FlashcardList: {ex.Message}");
            }
        }
    }
}
