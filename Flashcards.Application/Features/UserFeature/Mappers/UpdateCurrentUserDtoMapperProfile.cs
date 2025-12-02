using AutoMapper;
using Flashcards.Application.Features.UserFeature.DTOs.Requests;
using Flashcards.Application.Features.UserFeature.DTOs.Responses;
using Flashcards.Domain.Entities;

namespace Flashcards.Application.Features.UserFeature.Mappers
{
    public class UpdateCurrentUserDtoMapperProfile : Profile
    {
        public UpdateCurrentUserDtoMapperProfile()
        {
            CreateMap<UpdateCurrentUserDto, User>()
                .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) =>
                    srcMember != null && (!(srcMember is string) || !string.IsNullOrWhiteSpace((string)srcMember))
                )
            );

            CreateMap<User, UpdateCurrentUserResponseDto>();
        }
    }
}
