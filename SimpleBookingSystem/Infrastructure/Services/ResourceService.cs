using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleBookingSystemService.Infrastructure.Domain;
using SimpleBookingSystemService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Infrastructure.Services
{
    public class ResourceService : IResourceService
    {
        private readonly SimpleBookingSystemContext _context;
        private readonly IMapper _mapper;

        public ResourceService(SimpleBookingSystemContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ResourceModel>> GetResourcesAsync()
        {
            return _mapper.Map<List<ResourceModel>>(await _context.Resource.AsNoTracking().ToListAsync());
        }
    }
}
