using MediatR;
using SimpleBookingSystemService.Models;

namespace SimpleBookingSystemService.UseCases.Booking
{
    public class CreateBookingCommand : IRequest<CreateBookingResponseModel>
    {
        public CreateBookingRequestModel CreateBookingRequestModel { get; }
        public CreateBookingCommand(CreateBookingRequestModel createBookingRequestModel)
        {
            CreateBookingRequestModel = createBookingRequestModel;
        }
    }
}
