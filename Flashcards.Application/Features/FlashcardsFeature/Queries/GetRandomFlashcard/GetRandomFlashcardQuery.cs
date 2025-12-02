using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using MediatR;
using System.Security.Cryptography.X509Certificates;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetRandomFlashcard
{
    public class GetRandomFlashcardQuery : IRequest<OperationResult<FlashcardRandomResponseDto>>
    {
        public Guid FlashCardListId { get; set; }

        public GetRandomFlashcardQuery(Guid flashCardListId)
        {
            FlashCardListId = flashCardListId;
        }
    }
}
