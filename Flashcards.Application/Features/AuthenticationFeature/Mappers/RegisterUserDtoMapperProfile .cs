using AutoMapper;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Requests;
using Flashcards.Application.Features.AuthenticationFeature.DTOs.Responses;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.AuthenticationFeature.Mappers
{
    public class RegisterUserDtoMapperProfile : Profile
    {
        public RegisterUserDtoMapperProfile()
        {
            CreateMap<RegisterUserDto, User>();

            CreateMap<User, RegisterUserResponseDto>();
        }
    }
}
