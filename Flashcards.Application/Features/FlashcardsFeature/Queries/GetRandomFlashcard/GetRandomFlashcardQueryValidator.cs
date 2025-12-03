using FluentValidation;

namespace Flashcards.Application.Features.FlashcardsFeature.Queries.GetRandomFlashcard
{
    public class GetRandomFlashcardQueryValidator : AbstractValidator<GetRandomFlashcardQuery>
    {
        public GetRandomFlashcardQueryValidator()
        {
            RuleFor(x => x.FlashCardListId)
                .NotEmpty().WithMessage("FlashCardListId must be provided.");
        }
    }
}
