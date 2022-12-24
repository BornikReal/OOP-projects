using Application.Contracts.Workers;
using Application.ChainOfResponsibilities.WorkerModels;
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
        var command = new CreateWorker.Command(model.sessionId, new MasterWorkerModel(model.name, model.access), model.login, model.password);
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
