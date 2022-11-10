using CreditCardValidation.Commands;
using CreditCardValidation.Commands.SaveCreditCardCommand;
using CreditCardValidation.Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create([FromBody] SaveCreditCardCommandInput input)
    {
        return HandleResult(await _mediator.Send(input));
    }
}
