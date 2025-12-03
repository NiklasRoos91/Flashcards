using AutoMapper;
using Flashcards.Application.Features.FlashcardListsFeature.Queries.GetFlashcardListWithFlashcards;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Moq;

namespace Flashcards.Tests.FlashcardlistTests
{
    public class GetFlashcardsByListTests
    {
        private Mock<IFlashcardListRepository> _repositoryMock;
        private IMapper _mapper;
        private GetFlashcardListWithFlashcardsQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IFlashcardListRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flashcard, FlashcardResponseDto>()
                   .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.FlashcardTags.Select(ft => ft.Tag.Name).ToList()));
            });
            _mapper = config.CreateMapper();

            _handler = new GetFlashcardListWithFlashcardsQueryHandler(_repositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GetFlashcardsByList_ReturnsFlashcards_WhenListExists()
        {
            // Arrange
            var listId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var flashcards = new List<Flashcard>
            {
                new Flashcard { FlashcardId = Guid.NewGuid(), Question = "Q1", Answer = "A1", FlashcardTags = new List<FlashcardTag>() },
                new Flashcard { FlashcardId = Guid.NewGuid(), Question = "Q2", Answer = "A2", FlashcardTags = new List<FlashcardTag>() }
            };

            var flashcardList = new FlashcardList
            {
                FlashcardListId = listId,
                UserId = userId,
                Flashcards = flashcards
            };

            _repositoryMock.Setup(r => r.GetByIdWithFlashcardsAsync(listId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcardList);

            var query = new GetFlashcardListWithFlashcardsQuery(listId, userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data!.Count);
            Assert.AreEqual("Q1", result.Data[0].Question);
            Assert.AreEqual("A2", result.Data[1].Answer);
        }

        [Test]
        public async Task GetFlashcardsByList_ReturnsFailure_WhenListNotFound()
        {
            // Arrange
            var listId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetByIdWithFlashcardsAsync(listId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((FlashcardList?)null);

            var query = new GetFlashcardListWithFlashcardsQuery(listId, userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Flashcard list not found or access denied.", result.ErrorMessage);
        }
    }
}
