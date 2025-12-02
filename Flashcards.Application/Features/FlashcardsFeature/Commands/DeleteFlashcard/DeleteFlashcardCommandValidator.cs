using FluentValidation;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.DeleteFlashcard
{
    public class DeleteFlashcardCommandValidator : AbstractValidator<DeleteFlashcardCommand>
    {
        public DeleteFlashcardCommandValidator()
        {
            RuleFor(x => x.FlashcardId).NotEmpty().WithMessage("FlashcardId is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        }
    }
}
