using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystemService.Models;
using SimpleBookingSystemService.UseCases.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookingSystemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourceController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("GetResources")]
        [ProducesResponseType(typeof(List<ResourceModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ResourceModel>>> GetResources()
        {
            ResourceQuery resourceQuery = new ResourceQuery();
            return Ok(await _mediator.Send(resourceQuery));
        }
    }
}
