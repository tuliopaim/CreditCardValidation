using CreditCardValidation.Domain.Contracts;
using MediatR;

namespace CreditCardValidation.Commands.ValidateTokenCommand;

public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommandInput, ValidateTokenCommandResponse>
{
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly INotifier _notifier;

    public ValidateTokenCommandHandler(
        ICreditCardRepository creditCardRepository,
        IDateTimeProvider dateTimeProvider,
        INotifier notifier)
    {
        _creditCardRepository = creditCardRepository;
        _dateTimeProvider = dateTimeProvider;
        _notifier = notifier;
    }

    public async Task<ValidateTokenCommandResponse> Handle(ValidateTokenCommandInput request, CancellationToken cancellationToken)
    {
        var card = await _creditCardRepository.GetToValidateToken(request.CardId);
        if (card is null)
        {
            _notifier.Notify($"Credit Card not found!");
            return new();
        }

        var utcNow = _dateTimeProvider.UtcNow;
        if (utcNow - card.TokenCreatedAt is { Minutes: > 30 })
        {
            return new() { Validated = false };
        }

        if (card.CustomerId != request.CustomerId)
        {
            return new() { Validated = false };
        }

        var validToken = card.CreateToken(request.CVV, utcNow);
        if (validToken != request.Token)
        {
            return new() { Validated = false };
        }

        return new ValidateTokenCommandResponse { Validated = true };
    }
}
