using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.UserFeature.DTOs.Responses;
using MediatR;

namespace Flashcards.Application.Features.UserFeature.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<OperationResult<GetUserInfoDto>>
    {
        public Guid UserId { get; set; }

        public GetCurrentUserQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
