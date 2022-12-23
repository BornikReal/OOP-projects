using Application.Contracts.Messages;
using Application.Dto.Messages;
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
        string result = response.message switch
        {
            EmailMessageDto messageConc => $"{messageConc.sender}|{messageConc.messageSubject}|{messageConc.message}",
            PhoneMessageDto messageConc => $"{messageConc.phoneNumber}:{messageConc.message}",
            MessengerMessageDto messageConc => $"{messageConc.sender}:{messageConc.message}",
            _ => throw new Exception("Unknown message type"),
        };

        return Ok(result);
    }
}
