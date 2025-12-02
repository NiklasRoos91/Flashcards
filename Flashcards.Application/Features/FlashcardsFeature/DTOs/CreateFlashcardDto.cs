namespace Flashcards.Application.Features.FlashcardsFeature.DTOs
{
    public class CreateFlashcardDto
    {
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public Guid FlashcardListId { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}
