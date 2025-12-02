using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Validators
{
    public class CreateFlashcardDtoValidator : AbstractValidator<CreateFlashcardDto>
    {
        public CreateFlashcardDtoValidator()
        {
            RuleFor(x => x.Question)
                .NotEmpty()
                .WithMessage("Question cannot be empty.")
                .MaximumLength(500)
                .WithMessage("Question cannot exceed 500 characters.");

            RuleFor(x => x.Answer)
                .NotEmpty()
                .WithMessage("Answer cannot be empty.")
                .MaximumLength(500)
                .WithMessage("Answer cannot exceed 500 characters.");

            RuleFor(x => x.FlashcardListId)
                .NotEmpty()
                .WithMessage("Flashcard List ID cannot be empty.")
                .Must(id => id != Guid.Empty)
                .WithMessage("Flashcard List ID must be a valid GUID.");
        }
    }
}
