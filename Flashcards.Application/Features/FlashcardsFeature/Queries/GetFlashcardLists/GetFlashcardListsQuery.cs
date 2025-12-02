using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardLists
{
    public class GetFlashcardListsQuery : IRequest<OperationResult<IEnumerable<FlashcardListResponseDto>>>
    {
        public Guid UserId { get; }

        public GetFlashcardListsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
