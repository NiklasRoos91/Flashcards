using FluentValidation;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Commands.UpdateFlashcardList
{
    public class UpdateFlashcardListCommandValidator : AbstractValidator<UpdateFlashcardListCommand>
    {
        public UpdateFlashcardListCommandValidator(IValidator<UpdateFlashcardListDto> updateFlashcardListDtoValidator)
        {
            RuleFor(x => x.UpdateFlashcardListDto)
                .SetValidator(updateFlashcardListDtoValidator);
        }
    }
}
