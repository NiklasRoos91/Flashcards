using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.AuthenticationFeature.Commands.LoginUser;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flashcards.Tests.AuthenticationTests
{
    public class LoginUserTests
    {
        private Mock<IUserRepository> _repositoryMock;
        private IMapper _mapper;
        private Mock<IPasswordHasher<User>> _passwordHasherMock;
        private Mock<ITokenService> _jwtTokenGeneratorMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();
            _jwtTokenGeneratorMock = new Mock<ITokenService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, LoginUserResponseDto>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task LoginUserCommandHandler_ReturnsSuccess_WhenValidCredentials()
        {
            // Arrange
            var user = new User { Email = "test@example.com", PasswordHash = "hashed_pw" };
            var loginDto = new LoginUserDto { Email = "test@example.com", Password = "Password123!" };

            _repositoryMock.Setup(r => r.GetUserByEmailAsync(loginDto.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _passwordHasherMock.Setup(ph => ph.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password))
                .Returns(PasswordVerificationResult.Success);

            _jwtTokenGeneratorMock.Setup(j => j.GenerateToken(user)).Returns("jwt_token");

            var handler = new LoginUserCommandHandler(
                _repositoryMock.Object,
                _mapper,
                _passwordHasherMock.Object,
                _jwtTokenGeneratorMock.Object);

            // Act
            var result = await handler.Handle(new LoginUserCommand(loginDto), CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("jwt_token", result.Data!.Token);
        }

        [Test]
        public async Task LoginUserCommandHandler_ReturnsFailure_WhenUserNotFound()
        {
            // Arrange
            var loginDto = new LoginUserDto { Email = "nonexistent@example.com", Password = "Password123!" };

            _repositoryMock.Setup(r => r.GetUserByEmailAsync(loginDto.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            var handler = new LoginUserCommandHandler(
                _repositoryMock.Object,
                _mapper,
                _passwordHasherMock.Object,
                _jwtTokenGeneratorMock.Object);

            // Act
            var result = await handler.Handle(new LoginUserCommand(loginDto), CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            StringAssert.Contains("Invalid email or password", result.ErrorMessage);
        }

        [Test]
        public async Task LoginUserCommandHandler_ReturnsFailure_WhenPasswordIsIncorrect()
        {
            // Arrange
            var user = new User { Email = "test@example.com", PasswordHash = "hashed_pw" };
            var loginDto = new LoginUserDto { Email = "test@example.com", Password = "WrongPassword" };

            _repositoryMock.Setup(r => r.GetUserByEmailAsync(loginDto.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _passwordHasherMock.Setup(ph => ph.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password))
                .Returns(PasswordVerificationResult.Failed);

            var handler = new LoginUserCommandHandler(
                _repositoryMock.Object,
                _mapper,
                _passwordHasherMock.Object,
                _jwtTokenGeneratorMock.Object);

            // Act
            var result = await handler.Handle(new LoginUserCommand(loginDto), CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            StringAssert.Contains("Invalid email or password", result.ErrorMessage);
        }

    }
}
