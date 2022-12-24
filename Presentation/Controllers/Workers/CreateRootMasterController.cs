using Application.Contracts.Workers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Workers;

namespace Presentation.Controllers.Workers;

[ApiController]
[Route("api/[controller]")]
public class CreateRootMasterController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateRootMasterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateRootMasterModel model)
    {
        var command = new CreateRootMaster.Command(model.name, model.login, model.password);
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
