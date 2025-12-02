namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses
{
    public class CreateFlashcardListResponseDto
    {
        public Guid FlashcardListId { get; set; }
        public string Title { get; set; } = null!;
    }
}
