using Flashcards.Application.Commons.OperationResult;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.UserFeature.Commands.DeleteUser
{
    public class DeleteCurrentUserCommandHandler : IRequestHandler<DeleteCurrentUserCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<User> _userRepository;

        public DeleteCurrentUserCommandHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<OperationResult<bool>> Handle(DeleteCurrentUserCommand request, CancellationToken cancellationToken)
        {
            var deleteUser = await _userRepository.DeleteAsync(request.UserId, cancellationToken);

            if (!deleteUser)
            {
                return OperationResult<bool>.Failure("User not found or could not be deleted.");
            }

            return OperationResult<bool>.Success(deleteUser);
        }
    }
}
