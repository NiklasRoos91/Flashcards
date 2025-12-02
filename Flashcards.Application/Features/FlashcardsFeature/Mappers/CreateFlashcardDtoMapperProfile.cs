using AutoMapper;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.FlashcardsFeature.Mappers
{
    public class CreateFlashcardDtoMapperProfile : Profile
    {
        public CreateFlashcardDtoMapperProfile()
        {
            CreateMap<CreateFlashcardDto, Flashcard>();

            CreateMap<Flashcard, CreateFlashcardResponseDto>();
        }
    }
}
