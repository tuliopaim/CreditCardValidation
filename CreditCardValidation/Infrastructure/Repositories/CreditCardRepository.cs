using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Infrastructure.Repositories;

public class CreditCardRepository : ICreditCardRepository
{
    public void Add(CreditCard creditCard)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
