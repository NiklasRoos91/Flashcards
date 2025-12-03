using FluentValidation;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardById
{
    public class GetFlashcardByIdQueryValidator : AbstractValidator<GetFlashcardByIdQuery>
    {
        public GetFlashcardByIdQueryValidator()
        {
            RuleFor(x => x.FlashcardId)
                .NotEmpty()
                .WithMessage("Flashcard ID must be provided.");
        }
    }
}