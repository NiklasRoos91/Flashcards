using FluentValidation;

namespace Flashcards.Application.Features.FlashcardListsFeature.Queries.GetFlashcardListWithFlashcards
{
    public class GetFlashcardListWithFlashcardsQueryValidator
        : AbstractValidator<GetFlashcardListWithFlashcardsQuery>
    {
        public GetFlashcardListWithFlashcardsQueryValidator()
        {
            RuleFor(x => x.FlashcardListId)
                .NotEmpty()
                .WithMessage("FlashcardListId cannot be empty.");
        }
    }
}
