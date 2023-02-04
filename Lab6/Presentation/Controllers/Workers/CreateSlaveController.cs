using Application.Contracts.Workers;
using Application.ChainOfResponsibilities.WorkerModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Workers;

[ApiController]
[Route("api/[controller]")]
public class CreateSlaveController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateSlaveController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateSlaveWorkerModel model)
    {
        var command = new CreateWorker.Command(model.sessionId, new SlaveWorkerModel(model.name, model.access), model.login, model.password);
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
