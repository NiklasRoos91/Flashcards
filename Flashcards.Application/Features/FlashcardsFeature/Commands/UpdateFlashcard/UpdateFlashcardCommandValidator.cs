using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.UpdateFlashcard
{
    public class UpdateFlashcardCommandValidator : AbstractValidator<UpdateFlashcardCommand>
    {
        public UpdateFlashcardCommandValidator(IValidator<UpdateFlashcardDto> updateFlashcardDtoValidator)
        {
            RuleFor(x => x.FlashcardId)
                .NotEmpty().WithMessage("FlashcardId must be a valid GUID.");

            RuleFor(x => x.UpdateFlashcardDto)
                .SetValidator(updateFlashcardDtoValidator);
        }
    }
}
