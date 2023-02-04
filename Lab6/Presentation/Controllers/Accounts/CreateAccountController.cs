using Application.Contracts.Accounts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Accounts;

namespace Presentation.Controllers.Accounts;

[ApiController]
[Route("api/[controller]")]
public class CreateAccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateAccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateAccountModel model)
    {
        var command = new CreateAccount.Command(model.access);
        CreateAccount.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.accountId);
    }
}
