using AutoMapper;
using Flashcards.Application.Features.FlashcardlistsFeature.Commands.DeleteFlashcardList;
using Flashcards.Application.Features.FlashcardlistsFeature.Commands.UpdateFlashcardList;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using Flashcards.Application.Features.FlashcardlistsFeature.Mappers;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Moq;

namespace Flashcards.Tests
{
    public class FlashcardListTests
    {
        private Mock<IGenericRepository<FlashcardList>> _repositoryMock;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            // Setup repository mock
            _repositoryMock = new Mock<IGenericRepository<FlashcardList>>();

            // Setup AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UpdateFlashcardListMapperProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task UpdateFlashcardListCommandHandler_UpdatesTitleSuccessfully()
        {
            // Arrange
            var flashcardListId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var entity = new FlashcardList { FlashcardListId = flashcardListId, Title = "Old Title", UserId = userId };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardListId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _repositoryMock.Setup(r => r.UpdateAsync(entity, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var handler = new UpdateFlashcardListCommandHandler(_repositoryMock.Object, _mapper);
            var command = new UpdateFlashcardListCommand(new UpdateFlashcardListDto
            {
                FlashcardListId = flashcardListId,
                Title = "New Title"
            }, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.Title, Is.EqualTo("New Title"));
        }

        [Test]
        public async Task DeleteFlashcardListCommandHandler_DeletesSuccessfully()
        {
            // Arrange
            var flashcardListId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var entity = new FlashcardList { FlashcardListId = flashcardListId, Title = "List", UserId = userId };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardListId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _repositoryMock.Setup(r => r.DeleteAsync(flashcardListId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new DeleteFlashcardListCommandHandler(_repositoryMock.Object);
            var command = new DeleteFlashcardListCommand(flashcardListId, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.True);
        }
    }
}
