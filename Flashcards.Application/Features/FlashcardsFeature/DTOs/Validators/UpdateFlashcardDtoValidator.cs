using FluentValidation;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;

namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Validators
{
    public class UpdateFlashcardDtoValidator : AbstractValidator<UpdateFlashcardDto>
    {
        public UpdateFlashcardDtoValidator()
        {
            When(x => x.Question != null, () =>
            {
                RuleFor(x => x.Question)
                    .NotEmpty().WithMessage("Question cannot be empty.")
                    .MaximumLength(500).WithMessage("Question cannot exceed 500 characters.");
            });

            When(x => x.Answer != null, () =>
            {
                RuleFor(x => x.Answer)
                    .NotEmpty().WithMessage("Answer cannot be empty.")
                    .MaximumLength(500).WithMessage("Answer cannot exceed 500 characters.");
            });

            When(x => x.Tags != null, () =>
            {
                RuleForEach(x => x.Tags)
                    .NotEmpty().WithMessage("Tag cannot be empty.")
                    .MaximumLength(50).WithMessage("Tag cannot exceed 50 characters.");

                RuleFor(x => x.Tags)
                    .Must(tags => tags.Count <= 10)
                    .WithMessage("A flashcard can have at most 10 tags.");
            });
        }
    }
}
