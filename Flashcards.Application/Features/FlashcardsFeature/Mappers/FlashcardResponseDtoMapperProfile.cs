using AutoMapper;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.FlashcardsFeature.Mappers
{
    public class FlashcardResponseDtoMapperProfile : Profile
    {
        public FlashcardResponseDtoMapperProfile()
        {
            CreateMap<Flashcard, FlashcardResponseDto>()
                .ForMember(dest => dest.Tags,
                           opt => opt.MapFrom(src => src.FlashcardTags.Select(ft => ft.Tag.Name).ToList()));
        }
    }
}
