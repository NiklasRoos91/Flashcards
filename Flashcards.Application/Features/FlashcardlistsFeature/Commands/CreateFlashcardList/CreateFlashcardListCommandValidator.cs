using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using FluentValidation;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Commands.CreateFlashcardList
{
    public class CreateFlashcardListCommandValidator : AbstractValidator<CreateFlashcardListCommand>
    {
        public CreateFlashcardListCommandValidator(IValidator<CreateFlashcardListDto> createFlashcardlistValidator)
        {
            RuleFor(x => x.CreateFlashcardListDto)
                .SetValidator(createFlashcardlistValidator);
        }
    }
}
