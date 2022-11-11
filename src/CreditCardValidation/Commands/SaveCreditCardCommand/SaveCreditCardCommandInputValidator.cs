using FluentValidation;

namespace CreditCardValidation.Commands.SaveCreditCardCommand;

public class SaveCreditCardCommandInputValidator : AbstractValidator<SaveCreditCardCommandInput>
{
    public SaveCreditCardCommandInputValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.CardNumber).GreaterThan(0);
        RuleFor(x => x.CVV).InclusiveBetween(1, 999);
        RuleFor(x => x.CardNumberStr)
            .CreditCard()
            .WithMessage("'Card Number' is not a valid credit card number.");
    }
}
