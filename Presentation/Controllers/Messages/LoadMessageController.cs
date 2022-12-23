using Application.Contracts.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers.Messages;

public class LoadMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoadMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] LoadMessageModel model)
    {
        var command = new LoadMessages.Command(model.sessionId);
        LoadMessages.Response res = await _mediator.Send(command, CancellationToken);

        return Ok(res.messages);
    }
}
