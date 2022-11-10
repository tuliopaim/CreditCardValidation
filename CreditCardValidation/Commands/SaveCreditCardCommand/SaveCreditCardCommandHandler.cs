using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Domain.Entities;
using MediatR;

namespace CreditCardValidation.Commands.SaveCreditCardCommand;

public class SaveCreditCardCommandHandler : IRequestHandler<SaveCreditCardCommandInput, SaveCreditCardCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly INotifier _notifier;

    public SaveCreditCardCommandHandler(
        ICustomerRepository customerRepository,
        ICreditCardRepository creditCardRepository,
        INotifier notifier,
        IDateTimeProvider dateTimeProvider)
    {
        _customerRepository = customerRepository;
        _creditCardRepository = creditCardRepository;
        _notifier = notifier;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<SaveCreditCardCommandResponse> Handle(SaveCreditCardCommandInput request, CancellationToken cancellationToken)
    {
        if (!await _customerRepository.CustomerExists(request.CustomerId))
        {
            _notifier.Notify($"Customer {request.CustomerId} not found!");
            return new();
        }

        CreditCard? creditCard = await _creditCardRepository.GetByCardNumberToEdit(request.CardNumber);

        if (creditCard is null)
        {
            creditCard = new CreditCard(request.CustomerId, request.CardNumber);

            _creditCardRepository.Add(creditCard);
        }

        var token = creditCard.CreateToken(request.CVV, _dateTimeProvider.UtcNow);

        await _creditCardRepository.SaveChanges(cancellationToken);

        return new SaveCreditCardCommandResponse
        {
            Token = token,
            RegistrationDate = creditCard.TokenCreatedAt,
            CardId = creditCard.CardId,
        };
    }
}
