using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcard
{
    public class CreateFlashcardCommand : IRequest<OperationResult<CreateFlashcardResponseDto>>
    {
        public CreateFlashcardDto CreateFlashcardDto { get; }
        public Guid UserId { get; }

        public CreateFlashcardCommand(CreateFlashcardDto createFlashcardDto, Guid userId)
        {
            CreateFlashcardDto = createFlashcardDto ?? throw new ArgumentNullException(nameof(createFlashcardDto));
            UserId = userId;
        }
    }
}
