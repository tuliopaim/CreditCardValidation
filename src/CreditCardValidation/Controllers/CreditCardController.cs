using CreditCardValidation.Commands;
using CreditCardValidation.Commands.SaveCreditCardCommand;
using CreditCardValidation.Commands.ValidateTokenCommand;
using CreditCardValidation.Core.Contracts;
using CreditCardValidation.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditCardValidation.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditCardController : BaseController
{
    private readonly IMediator _mediator;

    public CreditCardController(IMediator mediator, INotifier notifier) : base(notifier)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CommandResponse<SaveCreditCardCommandResponse>), 200)]
    [ProducesResponseType(typeof(ErrorCommandResponse), 400)]
    public async Task<IActionResult> Create(
        [FromBody] SaveCreditCardCommandInput input,
        CancellationToken cancellationToken)
    {
        return HandleResponse(await _mediator.Send(input, cancellationToken));
    }

    [HttpGet("validate-token")]
    [ProducesResponseType(typeof(CommandResponse<ValidateTokenCommandResponse>), 200)]
    [ProducesResponseType(typeof(ErrorCommandResponse), 400)]
    public async Task<IActionResult> ValidateToken(
        [FromQuery] ValidateTokenCommandInput input,
        CancellationToken cancellationToken)
    {
        return HandleResponse(await _mediator.Send(input, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromServices] CreditCardDbContext creditCardDbContext)
    {
        return Ok(await creditCardDbContext.CreditCards.ToListAsync());
    }
}
