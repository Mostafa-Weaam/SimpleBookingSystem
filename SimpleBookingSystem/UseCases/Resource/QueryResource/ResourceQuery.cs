using MediatR;
using SimpleBookingSystemService.Models;
using System.Collections.Generic;

namespace SimpleBookingSystemService.UseCases.Resource
{
    public class ResourceQuery : IRequest<List<ResourceModel>>
    {
    }
}
