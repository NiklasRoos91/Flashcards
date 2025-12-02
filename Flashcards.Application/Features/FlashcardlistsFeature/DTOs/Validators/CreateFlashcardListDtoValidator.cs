using FluentValidation;

namespace Flashcards.Application.Features.FlashcardlistsFeature.DTOs.Validators
{
    public class CreateFlashcardListDtoValidator : AbstractValidator<CreateFlashcardListDto>
    {
        public CreateFlashcardListDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        }
    }
}
