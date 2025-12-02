namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses
{
    public class CreateFlashcardResponseDto
    {
        public Guid FlashcardId { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public Guid FlashcardListId { get; set; }
    }
}
