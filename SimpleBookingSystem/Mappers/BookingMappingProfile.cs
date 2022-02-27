using AutoMapper;
using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Models;

namespace SimpleBookingSystemService.Mappers
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile()
        {
            CreateMap<CreateBookingRequestModel, Booking>()
               .ForMember(destination => destination.BookedQuantity, options => options.MapFrom(source => source.Quantity));
        }
    }
}
