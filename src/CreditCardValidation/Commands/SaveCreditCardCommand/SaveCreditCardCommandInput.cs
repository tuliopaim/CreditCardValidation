using MediatR;

namespace CreditCardValidation.Commands.SaveCreditCardCommand;
public class SaveCreditCardCommandInput : IRequest<SaveCreditCardCommandResponse>
{
    public int CustomerId { get; set; }
    public long CardNumber { get; set; }
    public int CVV { get; set; }
}
