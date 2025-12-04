using AutoMapper;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.AuthenticationFeature.Mappers
{
    public class LoginDtoMapperProfile : Profile
    {
        public LoginDtoMapperProfile()
        {
            CreateMap<User, LoginUserResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
