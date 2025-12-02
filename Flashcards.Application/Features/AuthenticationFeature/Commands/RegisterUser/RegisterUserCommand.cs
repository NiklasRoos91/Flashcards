using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.AuthenticationFeature.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<OperationResult<RegisterUserResponseDto>>
    {
        public RegisterUserDto RegisterUserDto { get; set; } = null!;

        public RegisterUserCommand() {}
    }
}
