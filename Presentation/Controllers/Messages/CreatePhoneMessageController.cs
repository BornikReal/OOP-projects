using Application.Contracts.Messages;
using Application.СhainOfResponsibilities.MessageModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.MessageModels;

namespace Presentation.Controllers.Messages;

[ApiController]
[Route("api/[controller]")]
public class CreatePhoneMessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreatePhoneMessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreatePhoneMessageModel model)
    {
        var command = new CreateMessage.Command(new PhoneMessageModel(model.label, model.message, model.phoneNumber));
        CreateMessage.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.messageId);
    }
}
