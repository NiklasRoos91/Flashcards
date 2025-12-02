using Flashcards.Domain.Entities;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;

namespace Flashcards.Application.Features.FlashcardsFeature.Helpers
{
    public static class FlashcardPatchHelper
    {
        public static void ApplyPatch(Flashcard flashcard, UpdateFlashcardDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Question))
                flashcard.Question = dto.Question;

            if (!string.IsNullOrEmpty(dto.Answer))
                flashcard.Answer = dto.Answer;

            if (dto.Tags != null)
            {
                flashcard.FlashcardTags.Clear();
                foreach (var tagName in dto.Tags)
                {
                    flashcard.FlashcardTags.Add(new FlashcardTag
                    {
                        Tag = new Tag { Name = tagName },
                        Flashcard = flashcard
                    });
                }
            }
        }
    }
}
