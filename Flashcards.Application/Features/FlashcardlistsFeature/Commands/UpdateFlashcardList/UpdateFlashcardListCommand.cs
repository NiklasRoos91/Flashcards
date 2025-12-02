using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;

using MediatR;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Commands.UpdateFlashcardList
{
    public class UpdateFlashcardListCommand : IRequest<OperationResult<UpdateFlashcardListDto>>
    {
        public UpdateFlashcardListDto UpdateFlashcardListDto { get; set; } = null!;
        public Guid UserId { get; set; }

        public UpdateFlashcardListCommand(UpdateFlashcardListDto dto, Guid userId)
        {
            UpdateFlashcardListDto = dto ?? throw new ArgumentNullException(nameof(dto));
            UserId = userId;
        }
    }
}
