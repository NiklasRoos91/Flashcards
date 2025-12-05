using Flashcards.Application.Features.FlashcardsFeature.Commands.DeleteFlashcard;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Moq;

namespace Flashcards.Tests.UnitTests.FlashcardTests
{
    public class DeleteFlashcardTests
    {
        private Mock<IGenericRepository<Flashcard>> _repositoryMock;
        private Mock<IFlashcardRepository> _flashcardRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IGenericRepository<Flashcard>>();
            _flashcardRepositoryMock = new Mock<IFlashcardRepository>();
        }

        [Test]
        public async Task DeleteFlashcardCommandHandler_DeletesSuccessfully()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "Q",
                Answer = "A",
                FlashcardList = new FlashcardList { UserId = userId }
            };

            _flashcardRepositoryMock.Setup(r => r.GetByIdWithListAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcard);

            _repositoryMock.Setup(r => r.DeleteAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new DeleteFlashcardCommandHandler(_repositoryMock.Object, _flashcardRepositoryMock.Object);
            var command = new DeleteFlashcardCommand(flashcardId, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.True);
        }

        [Test]
        public async Task DeleteFlashcardCommandHandler_Unauthorized_Fails()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var otherUserId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "Q",
                Answer = "A",
                FlashcardList = new FlashcardList { UserId = otherUserId }
            };

            _flashcardRepositoryMock.Setup(r => r.GetByIdWithListAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcard);

            var handler = new DeleteFlashcardCommandHandler(_repositoryMock.Object, _flashcardRepositoryMock.Object);
            var command = new DeleteFlashcardCommand(flashcardId, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public async Task DeleteFlashcardCommandHandler_NotFound_Fails()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _flashcardRepositoryMock.Setup(r => r.GetByIdWithListAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new KeyNotFoundException());

            var handler = new DeleteFlashcardCommandHandler(_repositoryMock.Object, _flashcardRepositoryMock.Object);
            var command = new DeleteFlashcardCommand(flashcardId, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
        }
    }
}
