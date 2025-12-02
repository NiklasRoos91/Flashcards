using Flashcards.Application.Commons.OperationResult;
using MediatR;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Commands.DeleteFlashcardList
{
    public class DeleteFlashcardListCommand : IRequest<OperationResult<bool>>
    {
        public Guid FlashcardListId { get; set; }
        public Guid UserId { get; set; }

        public DeleteFlashcardListCommand(Guid flashcardListId, Guid userId)
        {
            FlashcardListId = flashcardListId;
            UserId = userId;
        }
    }
}
