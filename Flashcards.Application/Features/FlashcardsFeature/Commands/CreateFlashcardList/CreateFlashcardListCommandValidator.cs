using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcardList
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
