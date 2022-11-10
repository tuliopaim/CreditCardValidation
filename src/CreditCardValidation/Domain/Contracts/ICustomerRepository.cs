using CreditCardValidation.Domain.Entities;

namespace CreditCardValidation.Domain.Contracts;

public interface ICustomerRepository
{
    Task<bool> CustomerExists(int customerId);
    void Add(Customer customer);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
