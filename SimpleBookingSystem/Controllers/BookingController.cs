using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystemService.Models;
using SimpleBookingSystemService.UseCases.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateBookingResponseModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestModel createBookingRequestModel)
        {
            CreateBookingCommand createBookingCommand = new CreateBookingCommand(createBookingRequestModel);
            var createBookingResponseModel = await _mediator.Send(createBookingCommand);
            return Accepted(createBookingResponseModel);
        }
    }
}
