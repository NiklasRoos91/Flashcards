using AutoMapper;
using Flashcards.Application.Features.FlashcardsFeature.Commands.UpdateFlashcard;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Moq;

namespace Flashcards.Tests.UnitTests.FlashcardTests
{
    public class UpdateFlashcardTests
    {
        private Mock<IGenericRepository<Flashcard>> _repositoryMock;
        private IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IGenericRepository<Flashcard>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flashcard, FlashcardResponseDto>()
                   .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.FlashcardTags.Select(ft => ft.Tag.Name).ToList()));
            });
            _mapper = config.CreateMapper();
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
                FlashcardList = new FlashcardList { UserId = userId },
                FlashcardTags = new List<FlashcardTag>()
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

            var handler = new UpdateFlashcardCommandHandler(_repositoryMock.Object, _mapper);
            var command = new UpdateFlashcardCommand(flashcardId, updateDto, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("New Q", result.Data!.Question);
            Assert.AreEqual("New A", result.Data.Answer);
            Assert.AreEqual(2, result.Data.Tags.Count);
            Assert.Contains("Tag1", result.Data.Tags);
            Assert.Contains("Tag2", result.Data.Tags);
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

            var handler = new UpdateFlashcardCommandHandler(_repositoryMock.Object, _mapper);
            var command = new UpdateFlashcardCommand(flashcardId, updateDto, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Flashcard not found.", result.ErrorMessage);
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
                FlashcardList = new FlashcardList { UserId = Guid.NewGuid() }
            };

            var updateDto = new UpdateFlashcardDto { Question = "New Q", Answer = "New A" };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcard);

            var handler = new UpdateFlashcardCommandHandler(_repositoryMock.Object, _mapper);
            var command = new UpdateFlashcardCommand(flashcardId, updateDto, userId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("You are not authorized to update this flashcard.", result.ErrorMessage);
        }
    }
}
