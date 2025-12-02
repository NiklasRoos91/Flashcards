using FluentValidation;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Commands.DeleteFlashcardList
{
    public class DeleteFlashcardListCommandValidator : AbstractValidator<DeleteFlashcardListCommand>
    {
        public DeleteFlashcardListCommandValidator()
        {
            RuleFor(x => x.FlashcardListId)
                .NotEmpty().WithMessage("FlashcardListId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
