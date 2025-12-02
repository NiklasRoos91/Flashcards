namespace Flashcards.Domain.Entities
{
    public class Flashcard
    {
        public Guid FlashcardId { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid FlashcardListId { get; set; }
        public FlashcardList FlashcardList { get; set; } = null!;
    }
}
