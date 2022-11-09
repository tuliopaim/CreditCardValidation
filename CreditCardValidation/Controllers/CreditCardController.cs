using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidation.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditCardController : ControllerBase
{
    private readonly ILogger<CreditCardController> _logger;

    public CreditCardController(ILogger<CreditCardController> logger)
    {
        _logger = logger;
    }
}
