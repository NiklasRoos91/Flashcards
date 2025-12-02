using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.AuthenticationFeature.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IValidator<RegisterUserDto> registerUserDtoValidator)
        {
            RuleFor(x => x.RegisterUserDto)
                .SetValidator(registerUserDtoValidator);
        }
    }
}
