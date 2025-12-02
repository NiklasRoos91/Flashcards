using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using FluentValidation;

namespace Flashcards.Application.Features.AuthenticationFeature.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator(IValidator<LoginUserDto> loginDtoValidator)
        {
            RuleFor(x => x.LoginDto)
                .SetValidator(loginDtoValidator);
        }
    }
}
