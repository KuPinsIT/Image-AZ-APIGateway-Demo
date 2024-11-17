using ImageAZAPIGateway.Application.Query.Queries.Images;
using ImageAZAPIGateway.Application.Query.Queries.Images.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageAZAPIGateway.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("latest")]
        public async Task<ActionResult<ImageVm>> GetLatestImageAsync([FromQuery]GetLatestImageQuery query)
            => await _mediator.Send(query);

        [HttpGet("count")]
        public async Task<ActionResult<ImageSummaryVm>> CountImagesReceivedAsync([FromQuery] CountImagesReceivedQuery query)
            => await _mediator.Send(query);
    }
}
