using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Domain.Contracts;
public interface ICreditCardRepository
{
    Task<CreditCard?> Get(int id);
    void Add(CreditCard creditCard);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
