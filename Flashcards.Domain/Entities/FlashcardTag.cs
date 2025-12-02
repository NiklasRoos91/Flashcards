namespace Flashcards.Domain.Entities
{
    public class FlashcardTag
    {
        public Guid FlashcardId { get; set; }
        public Flashcard Flashcard { get; set; } = null!;

        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}