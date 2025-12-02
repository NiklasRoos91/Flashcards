using Flashcards.Application.Features.UserFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.UserFeature.Commands.UpdateCurrentUser
{
    internal class UpdateCurrentUserCommandValidator : AbstractValidator<UpdateCurrentUserCommand>
    {
        public UpdateCurrentUserCommandValidator(IValidator<UpdateCurrentUserDto> updateCurrentUserDtoValidator)
        {
            RuleFor(x => x.UpdateCurrentUserDto)
                .SetValidator(updateCurrentUserDtoValidator!);
        }
    }
}
