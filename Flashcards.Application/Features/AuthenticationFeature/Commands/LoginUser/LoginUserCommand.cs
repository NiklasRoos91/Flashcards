using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.AuthenticationFeature.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<OperationResult<LoginUserResponseDto>>
    {
        public LoginUserDto LoginDto { get; set; } = null!;

        public LoginUserCommand(LoginUserDto loginDto)
        {
            LoginDto = loginDto ?? throw new ArgumentNullException(nameof(loginDto));
        }
    }
}
