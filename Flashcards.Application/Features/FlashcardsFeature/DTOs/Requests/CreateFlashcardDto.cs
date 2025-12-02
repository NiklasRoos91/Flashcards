namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests
{
    public class CreateFlashcardDto
    {
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public Guid FlashcardListId { get; set; }
    }
}
