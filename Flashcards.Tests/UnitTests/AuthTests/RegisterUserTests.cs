using AutoMapper;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.AuthenticationFeature.Commands.RegisterUser;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flashcards.Tests.AuthenticationTests
{
    public class RegisterUserTests
    {
        private Mock<IGenericRepository<User>> _repositoryMock;
        private IMapper _mapper;
        private Mock<IPasswordHasher<User>> _passwordHasherMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IGenericRepository<User>>();
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterUserDto, User>();
                cfg.CreateMap<User, RegisterUserResponseDto>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task RegisterUserCommandHandler_RegistersSuccessfully()
        {
            // Arrange
            var registerDto = new RegisterUserDto
            {
                Email = "test@example.com",
                Password = "Password123!"
            };

            var handler = new RegisterUserCommandHandler(_repositoryMock.Object, _mapper, _passwordHasherMock.Object);

            _passwordHasherMock
                .Setup(ph => ph.HashPassword(It.IsAny<User>(), registerDto.Password))
                .Returns("hashed_password");

            // Act
            var result = await handler.Handle(new RegisterUserCommand(registerDto), CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(registerDto.Email, result.Data!.Email);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task RegisterUserCommandHandler_ThrowsFailure_OnException()
        {
            // Arrange
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("DB error"));

            var registerDto = new RegisterUserDto
            {
                Email = "test@example.com",
                Password = "Password123!"
            };

            var handler = new RegisterUserCommandHandler(_repositoryMock.Object, _mapper, _passwordHasherMock.Object);

            // Act
            var result = await handler.Handle(new RegisterUserCommand(registerDto), CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            StringAssert.Contains("DB error", result.ErrorMessage);
        }
    }
}
