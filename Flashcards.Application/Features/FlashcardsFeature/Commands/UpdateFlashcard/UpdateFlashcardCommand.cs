using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.UpdateFlashcard
{
    public class UpdateFlashcardCommand : IRequest<OperationResult<FlashcardResponseDto>>
    {
        public Guid FlashcardId { get; }
        public UpdateFlashcardDto UpdateFlashcardDto { get; }
        public Guid UserId { get; }

        public UpdateFlashcardCommand(Guid flashcardId, UpdateFlashcardDto dto, Guid userId)
        {
            FlashcardId = flashcardId;
            UpdateFlashcardDto = dto;
            UserId = userId;
        }
    }
}
