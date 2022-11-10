using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Domain.Entities;
using CreditCardValidation.Infrastructure.Notifier;
using MediatR;

namespace CreditCardValidation.Commands.SaveCreditCardCommand;

public class SaveCreditCardCommandHandler : IRequestHandler<SaveCreditCardCommandInput, SaveCreditCardCommandResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly INotifier _notifier;

    public SaveCreditCardCommandHandler(
        ICustomerRepository customerRepository,
        ICreditCardRepository creditCardRepository,
        INotifier notifier)
    {
        _customerRepository = customerRepository;
        _creditCardRepository = creditCardRepository;
        _notifier = notifier;
    }

    public async Task<SaveCreditCardCommandResponse> Handle(SaveCreditCardCommandInput request, CancellationToken cancellationToken)
    {
        if (!await _customerRepository.CustomerExists(request.CustomerId))
        {
            _notifier.Notify($"Customer {request.CustomerId} not found!");
            return new();
        }

        var registeredAt = DateTime.UtcNow;
        var creditCard = new CreditCard(request.CustomerId, request.CVV, request.CardNumber, registeredAt);

        _creditCardRepository.Add(creditCard);
        await _creditCardRepository.SaveChanges(cancellationToken);

        var token = creditCard.CreateToken();

        return new SaveCreditCardCommandResponse
        {
            Token = token,
            RegistrationDate = registeredAt,
            CardId = creditCard.CardId,
        };
    }
}
