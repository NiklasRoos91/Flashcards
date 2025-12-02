using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.UserFeature.DTOs.Requests;
using Flashcards.Application.Features.UserFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.UserFeature.Commands.UpdateCurrentUser
{
    public class UpdateCurrentUserCommand : IRequest<OperationResult<UpdateCurrentUserResponseDto>>
    {
        public UpdateCurrentUserDto UpdateCurrentUserDto { get;set; } = null!;
        public Guid UserId { get; set; }

        public UpdateCurrentUserCommand() {}
    }
}
