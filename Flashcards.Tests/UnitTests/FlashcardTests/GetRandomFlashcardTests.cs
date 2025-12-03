using AutoMapper;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Application.Features.FlashcardsFeature.Queries.GetRandomFlashcard;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Moq;
using NUnit.Framework;


namespace Flashcards.Tests.UnitTests.FlashcardTests
{
    public class GetRandomFlashcardTests
    {
        private Mock<IFlashcardRepository> _repositoryMock;
        private IMapper _mapper;
        private GetRandomFlashcardQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IFlashcardRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Flashcard, FlashcardResponseDto>()
                   .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.FlashcardTags != null ?
                       new List<string>() : new List<string>())); // Anpassa om du har tags
            });
            _mapper = config.CreateMapper();

            _handler = new GetRandomFlashcardQueryHandler(_repositoryMock.Object, _mapper);
        }

        [Test]
        public async Task Handle_ReturnsFlashcard_WhenFound()
        {
            // Arrange
            var flashcardId = Guid.NewGuid();
            var flashcardListId = Guid.NewGuid();
            var flashcard = new Flashcard
            {
                FlashcardId = flashcardId,
                Question = "Q?",
                Answer = "A.",
                FlashcardTags = new List<FlashcardTag>()
            };

            _repositoryMock.Setup(r => r.GetRandomFlashcardByFlashcardListIdAsync(flashcardListId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flashcard);

            var query = new GetRandomFlashcardQuery(flashcardListId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(flashcardId, result.Data!.FlashcardId);
            Assert.AreEqual("Q?", result.Data.Question);
        }

        [Test]
        public async Task Handle_ReturnsFailure_WhenNoFlashcardFound()
        {
            // Arrange
            var flashcardListId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetRandomFlashcardByFlashcardListIdAsync(flashcardListId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Flashcard?)null);

            var query = new GetRandomFlashcardQuery(flashcardListId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No flashcards found for the specified list.", result.ErrorMessage);
        }
    }
}
