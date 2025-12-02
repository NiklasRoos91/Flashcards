namespace Flashcards.Application.Features.FlashcardlistsFeature.DTOs
{
    public class UpdateFlashcardListDto
    {
        public Guid FlashcardListId { get; set; }
        public string Title { get; set; } = null!;
    }
}
