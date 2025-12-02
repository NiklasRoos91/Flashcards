using FluentValidation;

namespace Flashcards.Application.Features.FlashcardlistsFeature.DTOs.Validators
{
    public class UpdateFlashcardListDtoValidator : AbstractValidator<UpdateFlashcardListDto>
    {
        public UpdateFlashcardListDtoValidator()
        {
            RuleFor(x => x.FlashcardListId).NotEmpty();
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        }
    }
}
