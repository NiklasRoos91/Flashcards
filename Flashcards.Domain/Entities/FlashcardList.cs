namespace Flashcards.Domain.Entities
{
    public class FlashcardList
    {
        public Guid FlashcardListId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    }
}
