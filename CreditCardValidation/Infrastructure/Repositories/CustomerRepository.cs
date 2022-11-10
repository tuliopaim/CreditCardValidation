using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public void Add(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CustomerExists(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
