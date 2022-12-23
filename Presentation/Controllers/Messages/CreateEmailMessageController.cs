using Application.Contracts.Messages;
using Application.СhainOfResponsibilities.MessageModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.MessageModels;

namespace Presentation.Controllers.Messages;

[ApiController]
[Route("api/[controller]")]
public class CreateEmailMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateEmailMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateEmailMessageModel model)
    {
        var command = new CreateMessage.Command(new EmailMessageModel(model.label, model.message, model.sender, model.messageSubject));
        CreateMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.messageId);
    }
}
