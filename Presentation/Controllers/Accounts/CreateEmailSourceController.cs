using Application.Contracts.Accounts;
using Application.СhainOfResponsibilities.MessageSourceModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.MessageSourceModels;

namespace Presentation.Controllers.Accounts;

[ApiController]
[Route("api/[controller]")]
public class CreateEmailSourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateEmailSourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] CreateEmailMessageSourceModel model)
    {
        var command = new CreateSource.Command(model.accountId, new EmailMessageSourceModel(model.label));
        _ = await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}
