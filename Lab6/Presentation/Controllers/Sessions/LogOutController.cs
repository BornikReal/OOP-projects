using Application.Contracts.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Sessions;

namespace Presentation.Controllers.Sessions;

[ApiController]
[Route("api/[controller]")]
public class LogOutController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogOutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] LogOutModel model)
    {
        var command = new LogOut.Command(model.sessionId);
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
