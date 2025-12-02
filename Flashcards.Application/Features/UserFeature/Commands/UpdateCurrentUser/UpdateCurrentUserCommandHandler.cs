using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.UserFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;

namespace Flashcards.Application.Features.UserFeature.Commands.UpdateCurrentUser
{
    public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand, OperationResult<UpdateCurrentUserResponseDto>>
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IMapper _mapper;

        public UpdateCurrentUserCommandHandler(
            IGenericRepository<User> genericRepository,
            IMapper mapper)
        {
            _genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<OperationResult<UpdateCurrentUserResponseDto>> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateCurrentUserDto == null)
            {
                return OperationResult<UpdateCurrentUserResponseDto>.Failure("Update data is required.");
            }

            var user = await _genericRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                return OperationResult<UpdateCurrentUserResponseDto>.Failure($"User with ID {request.UserId} not found.");
            }

            _mapper.Map(request.UpdateCurrentUserDto, user);

            await _genericRepository.UpdateAsync(user, cancellationToken);

            var updatedUserDto = _mapper.Map<UpdateCurrentUserResponseDto>(user);

            return OperationResult<UpdateCurrentUserResponseDto>.Success(updatedUserDto);
        }
    }
}
