using CreditCardValidation.Domain.Contracts;
using CreditCardValidation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardValidation.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CreditCardDbContext _creditCardDbContext;

    public CustomerRepository(CreditCardDbContext creditCardDbContext)
    {
        _creditCardDbContext = creditCardDbContext;
    }

    public void Add(Customer customer)
    {
        _creditCardDbContext.Customers.Add(customer);
    }

    public Task<bool> CustomerExists(int customerId)
    {
        return _creditCardDbContext.Customers.AnyAsync(x => x.CustomerId == customerId);
    }

    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        return _creditCardDbContext.SaveChangesAsync(cancellationToken);
    }
}
