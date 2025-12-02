using Flashcards.Application.Commons.OperationResult;
using MediatR;

namespace Flashcards.Application.Features.UserFeature.Commands.DeleteUser
{
    public class DeleteCurrentUserCommand : IRequest<OperationResult<bool>>
    {
        public Guid UserId { get; set; } 

        public DeleteCurrentUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
