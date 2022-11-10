using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Domain.Contracts;
public interface ICreditCardRepository
{
    void Add(CreditCard creditCard);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
