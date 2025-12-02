namespace Flashcards.Domain.Entities
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Name { get; set; } = null!;

        // Navigation property for many-to-many with Flashcard
        public ICollection<FlashcardTag> FlashcardTags { get; set; } = new List<FlashcardTag>();
    }
}