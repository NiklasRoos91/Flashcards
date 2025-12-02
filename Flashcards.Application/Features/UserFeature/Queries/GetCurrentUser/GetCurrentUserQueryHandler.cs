using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.UserFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.UserFeature.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, OperationResult<GetUserInfoDto>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(
            IGenericRepository<User> userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OperationResult<GetUserInfoDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null)
            {
                return OperationResult<GetUserInfoDto>.Failure("User not found.");
            }

            var userInfo = _mapper.Map<GetUserInfoDto>(user);

            return OperationResult<GetUserInfoDto>.Success(userInfo);
        }
    }
}
