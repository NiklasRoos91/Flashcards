using AutoMapper;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using Flashcards.Application.Features.FlashcardlistsFeature.Queries.GetFlashcardLists;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Moq;

namespace Flashcards.Tests.FlashcardListTests
{
    public class GetFlashcardListsTests
    {
        private Mock<IFlashcardListRepository> _repositoryMock;
        private IMapper _mapper;
        private GetFlashcardListsQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IFlashcardListRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlashcardList, FlashcardListResponseDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetFlashcardListsQueryHandler(_repositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GetFlashcardLists_ReturnsListsSuccessfully()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var lists = new List<FlashcardList>
            {
                new FlashcardList { FlashcardListId = Guid.NewGuid(), Title = "List 1", UserId = userId, Flashcards = new List<Flashcard>() },
                new FlashcardList { FlashcardListId = Guid.NewGuid(), Title = "List 2", UserId = userId, Flashcards = new List<Flashcard>() }
            };

            _repositoryMock.Setup(r => r.GetAllFlashcardListsByUserIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(lists);

            var query = new GetFlashcardListsQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, result.Data!.Count());
            Assert.IsTrue(result.Data.Any(d => d.Title == "List 1"));
            Assert.IsTrue(result.Data.Any(d => d.Title == "List 2"));
        }

        [Test]
        public async Task GetFlashcardLists_ReturnsEmptyList_WhenNoListsFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetAllFlashcardListsByUserIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<FlashcardList>());

            var query = new GetFlashcardListsQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsEmpty(result.Data!);
        }
    }
}
