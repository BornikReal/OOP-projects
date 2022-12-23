using Application.Contracts.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Sessions;

namespace Presentation.Controllers.Sessions;

public class LogInController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogInController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] LogInModel model)
    {
        var command = new LogIn.Command(model.login, model.password);
        LogIn.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.sessionId);
    }
}
