using MediatR;
using SimpleBookingSystemService.Infrastructure.Services;
using SimpleBookingSystemService.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.UseCases.Resource
{
    public class ResourceQueryHandler : IRequestHandler<ResourceQuery, List<ResourceModel>>
    {
        private readonly IResourceService _resourceService;
        public ResourceQueryHandler(IResourceService resourceService)
        {
            _resourceService = resourceService ?? throw new ArgumentNullException(nameof(resourceService));
        }

        public async Task<List<ResourceModel>> Handle(ResourceQuery request, CancellationToken cancellationToken)
        {
            return await _resourceService.GetResourcesAsync();
        }
    }
}
