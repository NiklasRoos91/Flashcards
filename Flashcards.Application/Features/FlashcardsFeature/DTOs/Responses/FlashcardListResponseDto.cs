namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses
{
    public class FlashcardListResponseDto
    {
        public Guid FlashcardListId { get; set; }
        public string Title { get; set; } = null!;
        public int FlashcardCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
