using AutoMapper;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.FlashcardsFeature.Mappers
{
    public class FlashcardListResponseDtoMapperProfile : Profile 
    {
        public FlashcardListResponseDtoMapperProfile()
        {
            CreateMap<FlashcardList, FlashcardListResponseDto>();
        }
    }
}
