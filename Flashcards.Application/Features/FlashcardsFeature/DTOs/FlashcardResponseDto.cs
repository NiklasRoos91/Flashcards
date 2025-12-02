namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses
{
    public class FlashcardResponseDto
    {
        public Guid FlashcardId { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public List<string> Tags { get; set; } = new List<string>();
    }
}
