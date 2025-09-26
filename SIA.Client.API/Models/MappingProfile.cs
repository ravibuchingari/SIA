using AutoMapper;
using SIA.Domain.Entities;
using SIA.Infrastructure.DTO;

namespace SIA.Client.API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserVM, User>().ReverseMap();
            CreateMap<OrganizationVM, Organization>().ReverseMap();
        }
    }
}
