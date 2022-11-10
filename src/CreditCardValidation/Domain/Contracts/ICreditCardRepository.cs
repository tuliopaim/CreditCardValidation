using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Domain.Contracts;
public interface ICreditCardRepository
{
    Task<CreditCard?> GetToValidateToken(int id);
    Task<CreditCard?> GetByCardNumberToEdit(long cardNumber);
    void Add(CreditCard creditCard);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
