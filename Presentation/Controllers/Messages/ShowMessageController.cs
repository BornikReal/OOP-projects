using Application.Contracts.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers.Messages;

public class ShowMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShowMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] ShowMessageModel model)
    {
        var command = new ShowMessage.Command(model.sessionId, model.messageId);
        ShowMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.message);
    }
}
