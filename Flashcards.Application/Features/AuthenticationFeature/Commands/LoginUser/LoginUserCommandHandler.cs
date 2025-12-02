using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Flashcards.Application.Features.AuthenticationFeature.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<LoginUserResponseDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(
            IUserRepository repository,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator            )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        }

        public async Task<OperationResult<LoginUserResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByEmailAsync(request.LoginDto.Email, cancellationToken);

            if (user == null)
            {
                return OperationResult<LoginUserResponseDto>.Failure("Invalid email or password.");
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.LoginDto.Password);             // Verify the password

            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return OperationResult<LoginUserResponseDto>.Failure("Invalid email or password.");
            }

            var token = _jwtTokenGenerator.GenerateToken(user); // Generate JWT token

            var loginResponse = _mapper.Map<LoginUserResponseDto>(user); // Map the user to the response DTO
            loginResponse.Token = token; // Set the token in the response DTO

            return OperationResult<LoginUserResponseDto>.Success(loginResponse);
        }
    }
}
