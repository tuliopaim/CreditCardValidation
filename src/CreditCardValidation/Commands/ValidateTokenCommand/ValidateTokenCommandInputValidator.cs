using FluentValidation;

namespace CreditCardValidation.Commands.ValidateTokenCommand;

public class ValidateTokenCommandInputValidator : AbstractValidator<ValidateTokenCommandInput>
{
    public ValidateTokenCommandInputValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.CardId).GreaterThan(0);
        RuleFor(x => x.CVV).InclusiveBetween(1, 999);
    }
}
