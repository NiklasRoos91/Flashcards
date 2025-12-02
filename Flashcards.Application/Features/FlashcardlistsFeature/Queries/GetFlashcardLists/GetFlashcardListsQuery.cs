using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using MediatR;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Queries.GetFlashcardLists
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
