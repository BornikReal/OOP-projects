using Application.Contracts.Reports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers.Reports;

[ApiController]
[Route("api/[controller]")]
public class CreateReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateReportModel model)
    {
        var command = new CreateReport.Command(model.sessionId, model.time, model.duration);
        CreateReport.Response response = await _mediator.Send(command, CancellationToken);

        return Ok(response.report);
    }
}
