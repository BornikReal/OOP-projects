using Application.Contracts.Accounts;
using Application.ChainOfResponsibilities.MessageSourceModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.MessageSourceModels;

namespace Presentation.Controllers.Accounts;

[ApiController]
[Route("api/[controller]")]
public class CreateMessengerSourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateMessengerSourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateMessengerMessageSourceModel model)
    {
        var command = new CreateSource.Command(model.accountId, new MessengerMessageSourceModel(model.label));
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
