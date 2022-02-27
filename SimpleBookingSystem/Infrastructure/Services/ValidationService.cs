using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleBookingSystem.Extensions.ErrorHandling.Exceptions;
using SimpleBookingSystem.Extensions.Logging;
using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Infrastructure.Enums.ErrorCodes;
using SimpleBookingSystemService.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Infrastructure.Services
{
    public class ValidationService : IValidationService
    {
        private readonly SimpleBookingSystemContext _context;
        private readonly ILogger<ValidationService> _logger;

        public ValidationService(SimpleBookingSystemContext context,
            ILogger<ValidationService> logger, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ValidateBookingConflicts(CreateBookingRequestModel createBookingRequestModel)
        {
            var bookings = await _context.Booking.AsNoTracking()
                .Where(x => (createBookingRequestModel.FromDate >= x.FromDate && createBookingRequestModel.FromDate <= x.ToDate) ||
                            (createBookingRequestModel.ToDate >= x.FromDate && createBookingRequestModel.ToDate <= x.ToDate) &&
                             createBookingRequestModel.ResourceId == x.ResourceId).ToListAsync();

            if (bookings.Any())
            {
                _logger.LogInformation(LogEvents.InsertItem, $"Conflict Error. Conflict detected with requested period {createBookingRequestModel.FromDate} & {createBookingRequestModel.ToDate}");
                throw new ApiException(HttpStatusCode.Conflict, BookingErrorCodes.BookingPeriodConflict.ToString());
            }
        }

        public async Task<Resource> ValidateResource(int resourceId)
        {
            var resource = await _context.Resource.FirstOrDefaultAsync(x => x.Id == resourceId);
            if (resource == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, $"NotFound Error. Resource with Id {resourceId} does not exist");
                throw new NotFoundException();
            }

            return resource;
        }
    }
}
