using Application.Contracts.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers.Messages;

[ApiController]
[Route("api/[controller]")]
public class HandleMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public HandleMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] HandleMessageModel model)
    {
        var command = new HandleMessage.Command(model.sessionId, model.messageId);
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
