using FluentValidation;

namespace CreditCardValidation.Commands.SaveCreditCardCommand;

public class SaveCreditCardCommandInputValidator : AbstractValidator<SaveCreditCardCommandInput>
{
    public SaveCreditCardCommandInputValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.CardNumber).GreaterThan(0);
        RuleFor(x => x.CVV).InclusiveBetween(1, 999);
    }
}
