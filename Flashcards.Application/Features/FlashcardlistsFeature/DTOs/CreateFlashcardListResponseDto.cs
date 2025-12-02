namespace Flashcards.Application.Features.FlashcardlistsFeature.DTOs
{
    public class CreateFlashcardListResponseDto
    {
        public Guid FlashcardListId { get; set; }
        public string Title { get; set; } = null!;
    }
}
