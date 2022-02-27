using MediatR;
using SimpleBookingSystemService.Infrastructure.Services;
using SimpleBookingSystemService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.UseCases.Booking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingResponseModel>
    {
        private readonly IBookingService _bookingService;
        public CreateBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
        }

        public async Task<CreateBookingResponseModel> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _bookingService.CreateBookingAsync(request.CreateBookingRequestModel);
            return new CreateBookingResponseModel(booking.Id);
        }
    }
}
