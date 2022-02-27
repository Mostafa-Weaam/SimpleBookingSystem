using AutoMapper;
using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Models;

namespace SimpleBookingSystemService.Mappers
{
    public class ResourceMappingProfile : Profile
    {
        public ResourceMappingProfile()
        {
            CreateMap<Resource, ResourceModel>();
        }
    }
}
