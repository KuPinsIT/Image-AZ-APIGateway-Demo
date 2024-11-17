using ImageAZAPIGateway.Application.Command.Features.Images.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageAZAPIGateway.Server.Controllers.Public
{
    [ApiController]
    [Route("public/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddImage(AddImageCommand command)
            => await _mediator.Send(command);
    }
}
