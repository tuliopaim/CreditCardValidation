using MediatR;

namespace CreditCardValidation.Commands.ValidateTokenCommand;

public class ValidateTokenCommandInput : IRequest<ValidateTokenCommandResponse>
{
    public int CustomerId { get; set; }
    public int CardId { get; set; }
    public long Token { get; set; }
    public int CVV { get; set; }
}
