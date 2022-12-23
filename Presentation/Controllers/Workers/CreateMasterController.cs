using Application.Contracts.Workers;
using Application.СhainOfResponsibilities.WorkerModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Workers;

[ApiController]
[Route("api/[controller]")]
public class CreateMasterController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateMasterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateMasterWorkerModel model)
    {
        var command = new CreateRootMaster.Command(model.name, model.login, model.password);
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
