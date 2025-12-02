using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcard
{
    public class CreateFlashcardCommandValidator : AbstractValidator<CreateFlashcardCommand>
    {
        public CreateFlashcardCommandValidator(IValidator<CreateFlashcardDto> createFlashcardDtoValidator)
        {
            RuleFor(x => x.CreateFlashcardDto)
                .SetValidator(createFlashcardDtoValidator);
        }
    }
}
