using AutoMapper;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Mappers
{
    public class UpdateFlashcardListMapperProfile : Profile
    {
        public UpdateFlashcardListMapperProfile()
        {
            CreateMap<UpdateFlashcardListDto, FlashcardList>();
            CreateMap<FlashcardList, UpdateFlashcardListDto>();
        }
    }
}
