namespace Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests
{
    public class UpdateFlashcardDto
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public List<string>? Tags { get; set; }
    }
}
