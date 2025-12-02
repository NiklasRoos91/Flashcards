using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flashcards.Application.Features.FlashcardsFeature.Commands.UpdateFlashcard;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Flashcards.Tests.FlashcardTests
{
    public class UpdateFlashcardTests
    {
        private Mock<IGenericRepository<Flashcard>> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IGenericRepository<Flashcard>>();
        }

        [Test]
        public async Task UpdateFlashcardCommandHandler_UpdatesSuccessfully()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "Old Q",
                Answer = "Old A",
                FlashcardList = new FlashcardList { UserId = userId }
            };

            var updateDto = new UpdateFlashcardDto
            {
                Question = "New Q",
                Answer = "New A",
                Tags = new List<string> { "Tag1", "Tag2" }
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcard);

            _repositoryMock.Setup(r => r.UpdateAsync(flashcard, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new UpdateFlashcardCommandHandler(_repositoryMock.Object);
            var command = new UpdateFlashcardCommand(flashcardId, updateDto, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(flashcard.Question, Is.EqualTo("New Q"));
            Assert.That(flashcard.Answer, Is.EqualTo("New A"));
        }

        [Test]
        public async Task UpdateFlashcardCommandHandler_FlashcardNotFound_ReturnsFailure()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var updateDto = new UpdateFlashcardDto { Question = "Q", Answer = "A" };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Flashcard?)null);

            var handler = new UpdateFlashcardCommandHandler(_repositoryMock.Object);
            var command = new UpdateFlashcardCommand(flashcardId, updateDto, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Flashcard not found."));
        }

        [Test]
        public async Task UpdateFlashcardCommandHandler_UnauthorizedUser_ReturnsFailure()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "Q",
                Answer = "A",
                FlashcardList = new FlashcardList { UserId = Guid.NewGuid() } // annan user
            };

            var updateDto = new UpdateFlashcardDto { Question = "New Q", Answer = "New A" };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcard);

            var handler = new UpdateFlashcardCommandHandler(_repositoryMock.Object);
            var command = new UpdateFlashcardCommand(flashcardId, updateDto, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("You are not authorized to update this flashcard."));
        }
    }
}
