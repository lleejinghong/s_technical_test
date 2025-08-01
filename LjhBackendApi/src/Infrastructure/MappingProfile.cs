using AutoMapper;
using LjhBackendApi.Application.Features.ApplicationUsers.Commands.Register;
using LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetApplicationUser;
using LjhBackendApi.Application.Features.ApplicationUsers.Queries.GetByEmail;
using LjhBackendApi.Domain.Entities;

namespace LjhBackendApi.Infrastructure;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistrationDto, ApplicationUser>();

        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();

        CreateMap<ApplicationUser, GetByEmailResponseDto>();
    }
}
