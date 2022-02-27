using AutoMapper;
using Extensions.DatabaseExtensions;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Extensions.ErrorHandling.Exceptions;
using SimpleBookingSystem.Extensions.Logging;
using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Infrastructure.Enums.ErrorCodes;
using SimpleBookingSystemService.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly SimpleBookingSystemContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;
        private readonly IValidationService _validationService;

        public BookingService(SimpleBookingSystemContext context, 
            IMapper mapper, ILogger<BookingService> logger, IValidationService validationService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }
        public async Task<Booking> CreateBookingAsync(CreateBookingRequestModel createBookingRequestModel)
        {
            var resource = await _validationService.ValidateResource(createBookingRequestModel.ResourceId);
            if (createBookingRequestModel.Quantity >= resource.Quantity)
            {
                _logger.LogInformation(LogEvents.CheckError, $"BadRequest Error. Requested quantity exceeds available quantity");
                throw new ApiException(HttpStatusCode.BadRequest, BookingErrorCodes.QuantityExceedsAvailableQuantity.ToString());
            }

            await _validationService.ValidateBookingConflicts(createBookingRequestModel);

            var booking = _mapper.Map<Booking>(createBookingRequestModel);
            await _context.PostAsync(booking, _logger);

            resource.Quantity -= createBookingRequestModel.Quantity;
            await _context.PutAsync(resource, _logger);

            return booking;
        }
    }
}
