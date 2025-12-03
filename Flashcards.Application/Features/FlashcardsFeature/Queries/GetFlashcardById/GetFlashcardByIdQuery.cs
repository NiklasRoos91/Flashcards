using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardById
{
    public class GetFlashcardByIdQuery : IRequest<OperationResult<FlashcardResponseDto>>
    {
        public Guid FlashcardId { get; }
        public Guid UserId { get; }

        public GetFlashcardByIdQuery(Guid flashcardId, Guid userId)
        {
            FlashcardId = flashcardId;
            UserId = userId;
        }
    }
}