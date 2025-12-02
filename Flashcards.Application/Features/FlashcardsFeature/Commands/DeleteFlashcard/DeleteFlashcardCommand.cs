using Flashcards.Application.Commons.OperationResult;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.DeleteFlashcard
{
    public class DeleteFlashcardCommand : IRequest<OperationResult<bool>>
    {
        public Guid FlashcardId { get; set; }
        public Guid UserId { get; set; }

        public DeleteFlashcardCommand(Guid flashcardId, Guid userId)
        {
            FlashcardId = flashcardId;
            UserId = userId;
        }
    }
}
