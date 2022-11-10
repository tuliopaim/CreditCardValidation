using CreditCardValidation.Commands;
using CreditCardValidation.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidation.Controllers;

public abstract class BaseController : Controller
{
    private readonly INotifier _notifier;

    protected BaseController(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected IActionResult HandleResponse<T>(T result)
    {
        if (!_notifier.IsValid) return ErrorsBadRequest();

        return Ok(new CommandResponse<T>
        {
            Result = result
        });
    }

    private IActionResult ErrorsBadRequest()
    {
        return BadRequest(new ErrorCommandResponse
        {
            Errors = _notifier.Notifications.ToList()
        });
    }
}