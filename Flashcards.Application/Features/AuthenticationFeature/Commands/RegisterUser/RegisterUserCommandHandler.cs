using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Flashcards.Application.Features.AuthenticationFeature.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<RegisterUserResponseDto>>
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserCommandHandler(
            IGenericRepository<User> repository,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        public async Task<OperationResult<RegisterUserResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request.RegisterUserDto);
                user.PasswordHash = _passwordHasher.HashPassword(user, request.RegisterUserDto.Password);

                await _repository.AddAsync(user, cancellationToken);

                var responseDto = _mapper.Map<RegisterUserResponseDto>(user);
                return OperationResult<RegisterUserResponseDto>.Success(responseDto);
            }
            catch (Exception ex)
            {
                return OperationResult<RegisterUserResponseDto>.Failure(ex.Message);
            }
        }
    }
}
