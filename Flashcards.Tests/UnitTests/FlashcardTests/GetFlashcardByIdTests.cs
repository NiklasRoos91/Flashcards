using AutoMapper;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardById;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Moq;


namespace Flashcards.Tests.UnitTests.FlashcardTests
{
    public class GetFlashcardByIdTests
    {
        private Mock<IGenericRepository<Flashcard>> _repositoryMock;
        private IMapper _mapper;
        private GetFlashcardByIdQueryHandler _handler;

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

            _handler = new GetFlashcardByIdQueryHandler(_repositoryMock.Object, _mapper);
        }

        [Test]
        public async Task GetFlashcardById_ReturnsFlashcard_WhenExists()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "What is TDD?",
                Answer = "Test Driven Development",
                FlashcardList = new FlashcardList { UserId = userId }
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcard);

            var query = new GetFlashcardByIdQuery(flashcardId, userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.Question, Is.EqualTo("What is TDD?"));
            Assert.That(result.Data.Answer, Is.EqualTo("Test Driven Development"));
        }

        [Test]
        public async Task GetFlashcardById_ReturnsFailure_WhenNotFound()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Flashcard?)null);

            var query = new GetFlashcardByIdQuery(flashcardId, userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Flashcard not found or access denied."));
        }


        [Test]
        public async Task GetFlashcardById_ReturnsFailure_WhenIdIsEmpty()
        {
            // Arrange
            var query = new GetFlashcardByIdQuery(Guid.Empty, Guid.NewGuid());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Flashcard not found or access denied."));
        }

        [Test]
        public async Task GetFlashcardById_ReturnsSuccess_WhenFlashcardHasNoTags()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "Q",
                Answer = "A",
                FlashcardList = new FlashcardList { UserId = userId },
                FlashcardTags = new List<FlashcardTag>() // inga tags
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(flashcard);

            var query = new GetFlashcardByIdQuery(flashcardId, userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data.Tags.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetFlashcardById_ReturnsFailure_WhenUserUnauthorized()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "Q",
                Answer = "A",
                FlashcardList = new FlashcardList { UserId = Guid.NewGuid() } // annan user
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(flashcardId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(flashcard);

            var query = new GetFlashcardByIdQuery(flashcardId, Guid.NewGuid());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Flashcard not found or access denied."));
        }
    }
}
