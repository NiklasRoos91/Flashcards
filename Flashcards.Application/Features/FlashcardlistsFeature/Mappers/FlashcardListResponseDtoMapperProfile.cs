using AutoMapper;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Mappers
{
    public class FlashcardListResponseDtoMapperProfile : Profile 
    {
        public FlashcardListResponseDtoMapperProfile()
        {
            CreateMap<FlashcardList, FlashcardListResponseDto>();
        }
    }
}
