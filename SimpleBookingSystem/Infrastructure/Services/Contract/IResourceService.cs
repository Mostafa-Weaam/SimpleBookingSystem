using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Infrastructure.Services
{
    public interface IResourceService
    {
        Task<List<ResourceModel>> GetResourcesAsync();
    }
}
