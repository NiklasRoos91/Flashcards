using AutoMapper;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.FlashcardlistsFeature.Mappers
{
    public class CreateFlashcardListDtoMapperProfile : Profile
    {
        public CreateFlashcardListDtoMapperProfile()
        {
            CreateMap<CreateFlashcardListDto, FlashcardList>();
            CreateMap<FlashcardList, CreateFlashcardListResponseDto>();
        }
    }
}
