using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Models;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Infrastructure.Services
{
    public interface IValidationService
    {
        Task<Resource> ValidateResource(int resourceId);
        Task ValidateBookingConflicts(CreateBookingRequestModel createBookingRequestModel);
    }
}
