using Flashcards.Application.Commons.OperationResult;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;
using Flashcards.Application.Features.FlashcardsFeature.Helpers;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.UpdateFlashcard
{
    public class UpdateFlashcardCommandHandler : IRequestHandler<UpdateFlashcardCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<Flashcard> _flashcardRepository;

        public UpdateFlashcardCommandHandler(IGenericRepository<Flashcard> flashcardRepository)
        {
            _flashcardRepository = flashcardRepository;
        }

        public async Task<OperationResult<bool>> Handle(UpdateFlashcardCommand request, CancellationToken cancellationToken)
        {
            var flashcard = await _flashcardRepository.GetByIdAsync(request.FlashcardId, cancellationToken);
            if (flashcard == null)
                return OperationResult<bool>.Failure("Flashcard not found.");

            if (flashcard.FlashcardList.UserId != request.UserId)
                return OperationResult<bool>.Failure("You are not authorized to update this flashcard.");

            FlashcardPatchHelper.ApplyPatch(flashcard, request.UpdateFlashcardDto);

            await _flashcardRepository.UpdateAsync(flashcard, cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}
