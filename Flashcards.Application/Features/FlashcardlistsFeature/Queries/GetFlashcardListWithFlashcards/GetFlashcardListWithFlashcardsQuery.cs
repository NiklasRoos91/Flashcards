using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.FlashcardListsFeature.Queries.GetFlashcardListWithFlashcards
{
    public class GetFlashcardListWithFlashcardsQuery : IRequest<OperationResult<List<FlashcardResponseDto>>>
    {
        public Guid FlashcardListId { get; }
        public Guid UserId { get; }

        public GetFlashcardListWithFlashcardsQuery(Guid flashcardListId, Guid userId)
        {
            FlashcardListId = flashcardListId;
            UserId = userId;
        }
    }
}