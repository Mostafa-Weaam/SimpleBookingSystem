using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Models;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Infrastructure.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(CreateBookingRequestModel createBookingRequestModel);
    }
}
