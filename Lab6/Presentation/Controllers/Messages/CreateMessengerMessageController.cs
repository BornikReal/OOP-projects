using Application.Contracts.Messages;
using Application.ChainOfResponsibilities.MessageModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.MessageModels;

namespace Presentation.Controllers.Messages;

[ApiController]
[Route("api/[controller]")]
public class CreateMessengerMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateMessengerMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateMessengerMessageModel model)
    {
        var command = new CreateMessage.Command(new MessengerMessageModel(model.label, model.message, model.sender));
        CreateMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.messageId);
    }
}
