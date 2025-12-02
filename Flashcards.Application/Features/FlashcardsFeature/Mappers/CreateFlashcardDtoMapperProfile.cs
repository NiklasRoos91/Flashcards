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
            CreateMap<CreateFlashcardDto, Flashcard>()
                .ForMember(dest => dest.FlashcardTags, opt => opt.Ignore()); 

            CreateMap<Flashcard, CreateFlashcardResponseDto>()
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.FlashcardTags.Select(ft => ft.Tag.Name).ToList()));

        }
    }
}
